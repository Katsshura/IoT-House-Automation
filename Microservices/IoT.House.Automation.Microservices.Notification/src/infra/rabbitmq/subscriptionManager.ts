import { injectable } from "inversify";
import { IEventHandler } from "../../domain/interfaces/IEventHandler";
import { Event } from "../../domain/entities/events/event";

@injectable()
export class SubscriptionManager {

    private _handlers: Record<string, IEventHandler<Event>>;

    constructor() {
        this._handlers = {};
    }

    public onEventAdded : (eventName: string) => void = () => {};
    public onEventRemoved: (eventName: string) => void = () => {};

    public addSubscription<TEventHandler extends IEventHandler<Event>>(event: string, eventHandler: TEventHandler): void {
        this._handlers[event] = eventHandler;
        this.onEventAdded(event);
    }

    public removeSubscription(event: string) {
        delete this._handlers[event];
        this.onEventRemoved(event);
    }

    public hasSubscriptionForEvent(event: string) : boolean {
        return this._handlers[event] ? true : false;
    }

    public getHandlerForEvent(event: string) : IEventHandler<Event> {
        return this._handlers[event];
    }
}