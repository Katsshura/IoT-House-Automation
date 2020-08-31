import { IEventHandler } from "../interfaces/IEventHandler";
import { PushNotificationEvent } from "../entities/events/pushNotificationEvent";
import { inject, injectable } from "inversify";
import { Types } from "../../container/types";
import { IPushSender } from "../interfaces/IPushSender";

@injectable()
export class PushNotificationEventHandler
  implements IEventHandler<PushNotificationEvent> {
  constructor(@inject(Types.IPushSender) private _pushSender: IPushSender) {}

  queue(): string {
    return "PushNotificationEvent";
  }

  handle(event: PushNotificationEvent): Promise<boolean> {
    switch (event.notification.pushType) {
      case "data":
        return this._pushSender.sendData(event.notification);
      case "notification":
        return this._pushSender.sendNotification(event.notification);
      default:
        throw Error("Unsupported push notification type");
    }
  }
}
