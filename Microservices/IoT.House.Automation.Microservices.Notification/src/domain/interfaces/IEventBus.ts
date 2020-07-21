import { Event } from "../entities/events/event";
import { IEventHandler } from "./IEventHandler";

export interface IEventBus {
    publish(event : Event) : void;
    subscribe<TEventHandler extends IEventHandler<Event>>(event: string, eventHandler: TEventHandler) : void;
    unsubscribe(event: string) : void;
}