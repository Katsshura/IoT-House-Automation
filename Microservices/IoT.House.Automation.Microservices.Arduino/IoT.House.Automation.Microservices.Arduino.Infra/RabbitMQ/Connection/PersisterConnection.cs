using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using IoT.House.Automation.Microservices.Arduino.Infra.RabbitMQ.Config;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace IoT.House.Automation.Microservices.Arduino.Infra.RabbitMQ.Connection
{
    public class PersisterConnection :
        IDisposable
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly int _retryCount;
        private IConnection _connection;
        private bool _disposed;

        private object _syncRoot = new object();

        public RabbitMQConfig Config { get; }

        public PersisterConnection(RabbitMQConfig mqConfig, int retryCount = 5)
        {
            Config = mqConfig;
            _retryCount = retryCount;
            
            _connectionFactory = new ConnectionFactory { Uri = new Uri(Config.Uri) };
        }

        private bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

        public IModel CreateModel()
        {
            if (IsConnected) return _connection.CreateModel();
            
            if (!TryConnect()) throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            _connection.Dispose();
        }

        private bool TryConnect()
        {

            lock (_syncRoot)
            {
                var policy = Policy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {
                    }
                );

                policy.Execute(() =>
                {
                    _connection = _connectionFactory.CreateConnection();
                });

                if (!IsConnected) return false;

                _connection.ConnectionShutdown += OnConnectionShutdown;
                _connection.CallbackException += OnCallbackException;
                _connection.ConnectionBlocked += OnConnectionBlocked;


                return true;

            }
        }

        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;
            TryConnect();
        }

        void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;
            TryConnect();
        }

        void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (_disposed) return;
            TryConnect();
        }
    }
}
