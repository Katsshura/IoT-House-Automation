import { connect, Connection } from "amqplib";
import { rabbitConfig } from "../../config/rabbitmq";
import { injectable } from "inversify";

@injectable()
export class RabbitMqConnection {
    private _exchange: string = (rabbitConfig.exchange || "");
    private _exchangeType: string = (rabbitConfig.exchangeType || "fanout");
    private _uri : string = (rabbitConfig.uri || "amqp://localhost");

    constructor(){}

    public async getConnection() : Promise<Connection | undefined> {
        return await connect(this._uri);
    }

    public getExchange() : string {
        return this._exchange;
    }

    public getExchangeType() : string {
        return this._exchangeType;
    }
}