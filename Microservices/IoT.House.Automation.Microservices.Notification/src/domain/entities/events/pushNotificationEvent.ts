import { Event } from "./event";
import { PushNotification } from "../models/pushNotification";

export class PushNotificationEvent extends Event {
    
    public constructor(private _notification: PushNotification) {
        super();
    }

    public getType(): string {
        return "PushNotificationEvent";
    }

    get notification() {
        return this._notification;
    }
}