require("dotenv").config();

const rabbitConfig = {
    protocol: process.env.RABBITMQ_PROTOCOL,
    host: process.env.RABBITMQ_HOST,
    port: process.env.RABBITMQ_PORT,
    user: process.env.RABBITMQ_USER,
    pass: process.env.RABBITMQ_PASS,
    uri: process.env.RABBITMQ_URI,
    vhost: process.env.RABBITMQ_VHOST,
    exchange: process.env.RABBITMQ_EXCHANGE,
    exchangeType: process.env.RABBITMQ_EXCHANGE_TYPE
}

export { rabbitConfig };