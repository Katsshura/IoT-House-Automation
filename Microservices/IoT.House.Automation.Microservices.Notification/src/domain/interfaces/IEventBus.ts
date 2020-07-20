import { Event } from "../entities/events/event";
import { IEventHandler } from "./IEventHandler";

export interface IEventBus {
    publish(event : Event) : void;
    subscribe<TEvent extends Event, TEventHandler extends IEventHandler<TEvent>>() : void;
    unsubscribe<TEvent extends Event, TEventHandler extends IEventHandler<TEvent>>() : void;
}