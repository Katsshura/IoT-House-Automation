using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using IoT.House.Automation.Microservices.Arduino.Application.Interfaces;
using IoT.House.Automation.Microservices.Arduino.Domain.Interfaces;

namespace IoT.House.Automation.Microservices.Arduino.Application.Services
{
    public class HeartbeatService : IHeartbeat
    {
        private readonly IArduino _arduino;

        public HeartbeatService(IArduino arduino)
        {
            _arduino = arduino;
        }

        public Task Start()
        {
            var arduinos = _arduino.GetArduinos();
            foreach (var arduinoInfo in arduinos)
            {
                VerifyServerAvailability(arduinoInfo.IP, arduinoInfo.Port);
            }
            return Task.CompletedTask;
        }

        private bool VerifyServerAvailability(IPAddress ip, int port)
        {
            Ping ping = null;
            var result = true;

            try
            {
                ping = new Ping();
                var reply = ping.Send(ip);
                result &= reply?.Status == IPStatus.Success;
                result &= VerifyPortAvailability(ip, port);

                return result;
            }
            catch
            {
                return false;
            }
            finally
            {
                ping?.Dispose();
            }
        }

        private bool VerifyPortAvailability(IPAddress ip, int port)
        {
            var scanner = new TcpClient();

            try
            {
                scanner.SendTimeout = 3000;
                scanner.ReceiveTimeout = 3000;
                scanner.Connect(ip, port);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                scanner.Close();
            }
        }
    }
}
