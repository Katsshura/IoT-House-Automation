import { IPushSender } from "../../domain/interfaces/IPushSender";
import { PushNotification } from "../../domain/entities/models/pushNotification";
import { injectable, inject } from "inversify";
import { messaging } from "firebase-admin";
import { FCMConnection } from "./FCMConnection";

@injectable()
export class FCMSender implements IPushSender {
  private _fcmApp: messaging.Messaging;

  constructor(@inject(FCMConnection) private fcmConnection: FCMConnection) {
    this._fcmApp = fcmConnection.getInstance().messaging();
  }

  async sendNotification(notification: PushNotification): Promise<boolean> {
    var result = await this._fcmApp.sendToDevice(notification.token, {
      notification: {
        title: notification.title,
        body: notification.body,
        clickAction: notification.onClick,
        icon: notification.icon,
      },
    });
    return new Promise<boolean>((res) =>
      res(result.results[0].error ? false : true)
    );
  }
  async sendData(notification: PushNotification): Promise<boolean> {
    var result = await this._fcmApp.sendToDevice(notification.token, {
      data: {
        title: notification.title,
        body: notification.body,
      },
    });

    return new Promise<boolean>((res) =>
      res(result.results[0].error ? false : true)
    );
  }
}