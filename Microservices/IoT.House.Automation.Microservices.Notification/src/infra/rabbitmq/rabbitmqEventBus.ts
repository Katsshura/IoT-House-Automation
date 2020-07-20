import { RabbitMqConnection } from "../rabbitmq/rabbitmqConnection";
import { Connection, Channel } from "amqplib";
import { IEventBus } from "../../domain/interfaces/IEventBus";
import { Event } from "../../domain/entities/events/event";
import { IEventHandler } from "../../domain/interfaces/IEventHandler";
import { injectable, inject } from "inversify";
import { Types } from "../../container/types";
import { SubscriptionManager } from "./subscriptionManager";
import { DependencyResolver } from "../../container/di-container";

@injectable()
export class RabbitMqEventBus implements IEventBus {
    private _connection : Connection | undefined;
    private _channel : Channel | undefined;

    constructor(
       @inject(SubscriptionManager) private _subscriptionManager: SubscriptionManager,
       @inject(RabbitMqConnection) private _rabbitCon: RabbitMqConnection
    ){}

    public async publish(event: Event) : Promise<void> {

        await this.createChannel();
        
        var exchange = this._rabbitCon.getExchange();
        var eventName = event.getType();
        this._channel?.assertExchange(exchange, this._rabbitCon.getExchangeType(), { durable: false });
        this._channel?.publish(exchange, eventName, Buffer.from(JSON.stringify(event)));
    }

    public subscribe<TEvent extends Event, TEventHandler extends IEventHandler<TEvent>>(): void {
        DependencyResolver.getInstance().get<TEventHandler>("")
        throw new Error("Method not implemented.");
    }
    
    public unsubscribe<TEvent extends Event, TEventHandler extends IEventHandler<TEvent>>(): void {
        throw new Error("Method not implemented.");
    }

    private async createChannel() {
        this._connection = await this._rabbitCon.getConnection();
        this._channel = await this._connection?.createChannel();
    }
}