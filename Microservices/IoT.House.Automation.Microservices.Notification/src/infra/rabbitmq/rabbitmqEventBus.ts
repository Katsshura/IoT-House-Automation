import { RabbitMqConnection } from "../rabbitmq/rabbitmqConnection";
import { Connection, Channel, ConsumeMessage } from "amqplib";
import { IEventBus } from "../../domain/interfaces/IEventBus";
import { Event } from "../../domain/entities/events/event";
import { IEventHandler } from "../../domain/interfaces/IEventHandler";
import { injectable, inject } from "inversify";
import { SubscriptionManager } from "./subscriptionManager";

@injectable()
export class RabbitMqEventBus implements IEventBus {
    private _connection : Connection | undefined;
    private _channel : Channel | undefined;

    constructor(
       @inject(SubscriptionManager) private _subscriptionManager: SubscriptionManager,
       @inject(RabbitMqConnection) private _rabbitCon: RabbitMqConnection
    ){
        _subscriptionManager.onEventAdded = this.onSubscriptionManagerEventAdded.bind(this);
        _subscriptionManager.onEventRemoved = this.onSubscriptionManagerEventRemoved.bind(this);
    }

    public async publish(event: Event) : Promise<void> {

        await this.createChannel();
        
        var exchange = this._rabbitCon.getExchange();
        var eventName = event.getType();
        this.assertExchange(exchange);
        this._channel?.publish(exchange, eventName, Buffer.from(JSON.stringify(event)));
    }

    public subscribe<TEventHandler extends IEventHandler<Event>>(event: string, eventHandler: TEventHandler): void {
        this._subscriptionManager.addSubscription(event, eventHandler);
    }
    
    public unsubscribe(event: string): void {
        this._subscriptionManager.removeSubscription(event);
    }

    private async onSubscriptionManagerEventAdded(eventName: string) {
        await this.createChannel();

        var exchange = this._rabbitCon.getExchange();

        this._channel?.assertQueue(eventName, { durable: true, exclusive: false });
        this.assertExchange(exchange)
        this._channel?.bindQueue(eventName, exchange, eventName);
        this._channel?.consume(eventName, (msg) => this.handler(msg), { noAck: false });
    }

    private async onSubscriptionManagerEventRemoved(eventName: string) {
        await this.createChannel();

        this._channel?.unbindQueue(eventName, this._rabbitCon.getExchange(), eventName);
        this._channel?.cancel(eventName);
    }

    private handler(message: ConsumeMessage | null) {
        if(!message) { return; }

        var eventName = (message.fields.routingKey || "");
        var content = this.getJsonString((message.content || Buffer.from("")));
        var parsed = JSON.parse(JSON.parse(content)) as Event;

        if(!this._subscriptionManager.hasSubscriptionForEvent(eventName)) { return; }

        var handler = this._subscriptionManager.getHandlerForEvent(eventName);
        
        handler.handle(parsed).then((res) => {
            res ? this._channel?.ack(message) : this._channel?.nack(message);
        });
    }

    private async createChannel() {
        if(!this._connection){
            this._connection = await this._rabbitCon.getConnection();
        }
        
        if(!this._channel){
            this._channel = await this._connection?.createChannel();
        }
    }

    private assertExchange(exchange: string) {
        this._channel?.assertExchange(exchange, this._rabbitCon.getExchangeType(), { durable: true });
    }

    private getJsonString(content: Buffer) : string {
        var result = content.toString();
        result = result.replace(/\\n/g, "\\n")  
                        .replace(/\\'/g, "\\'")
                        .replace(/\\"/g, '\\"')
                        .replace(/\\&/g, "\\&")
                        .replace(/\\r/g, "\\r")
                        .replace(/\\t/g, "\\t")
                        .replace(/\\b/g, "\\b")
                        .replace(/\\f/g, "\\f");
        result = result.replace(/[\u0000-\u0019]+/g,"");
        result = JSON.stringify(result);
        return result;
    }
}