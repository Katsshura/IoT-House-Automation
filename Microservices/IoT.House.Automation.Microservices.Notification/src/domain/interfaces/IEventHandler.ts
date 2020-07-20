import { Event } from "../entities/events/event";

export interface IEventHandler<TEvent extends Event> {
    handle(event: TEvent) : Promise<boolean>;
}