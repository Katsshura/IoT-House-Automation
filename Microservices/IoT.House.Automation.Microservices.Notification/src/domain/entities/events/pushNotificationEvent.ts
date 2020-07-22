import { Event } from "./event";

export class PushNotificationEvent extends Event {
    
    public constructor(private _message: string) {
        super();
    }

    public getType(): string {
        return "PushNotificationEvent";
    }

    public getMessage() : string {
        return this._message;
    }
}