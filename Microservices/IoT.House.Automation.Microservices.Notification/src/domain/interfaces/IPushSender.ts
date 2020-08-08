import { PushNotification } from "../entities/models/pushNotification";

export interface IPushSender {
    sendNotification(notification : PushNotification) : Promise<boolean>;
    sendData(notification : PushNotification) : Promise<boolean>;
}