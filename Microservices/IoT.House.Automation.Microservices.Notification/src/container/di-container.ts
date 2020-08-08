import { Container } from "inversify";
import { RabbitMqConnection } from "../infra/rabbitmq/rabbitmqConnection";
import { RabbitMqEventBus } from "../infra/rabbitmq/rabbitmqEventBus";
import { IEventBus } from "../domain/interfaces/IEventBus";
import getDecorators from "inversify-inject-decorators";
import { Types } from "./types";
import { SubscriptionManager } from "../infra/rabbitmq/subscriptionManager";
import { IPushSender } from "../domain/interfaces/IPushSender";
import { FCMSender } from "../infra/firebase/FCMSender";
import { IEventHandler } from "../domain/interfaces/IEventHandler";
import { PushNotification } from "../domain/entities/models/pushNotification";
import { PushNotificationEvent } from "../domain/entities/events/pushNotificationEvent";
import { PushNotificationEventHandler } from "../domain/handlers/pushNotificationEventHandler";
import { FCMConnection } from "../infra/firebase/FCMConnection";

export class DependencyResolver {
  private static DIContainer: Container;

  private constructor() {}

  public static getInstance(): Container {
    if (!this.DIContainer) {
      this.DIContainer = new Container();
      this.ConfigureContainer();
    }

    return this.DIContainer;
  }

  private static ConfigureContainer() {
    this.ConfigureMessageBroker();
    this.ConfigureFCM();
    this.ConfigureHandlers();
  }

  private static ConfigureMessageBroker() {
    this.DIContainer.bind<IEventBus>(Types.IEventBus)
      .to(RabbitMqEventBus)
      .inSingletonScope();
    this.DIContainer.bind<SubscriptionManager>(SubscriptionManager)
      .toSelf()
      .inSingletonScope();
    this.DIContainer.bind<RabbitMqConnection>(RabbitMqConnection)
      .toSelf()
      .inSingletonScope();
  }

  private static ConfigureFCM() {
    this.DIContainer.bind<FCMConnection>(FCMConnection)
      .toSelf()
      .inSingletonScope();
    this.DIContainer.bind<IPushSender>(Types.IPushSender)
      .to(FCMSender)
      .inSingletonScope();
  }

  private static ConfigureHandlers() {
    this.DIContainer.bind<PushNotificationEventHandler>(Types.IEventHandler)
      .to(PushNotificationEventHandler)
      .inSingletonScope();
  }
}
