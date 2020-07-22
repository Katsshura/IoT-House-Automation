import { Container } from "inversify";
import { RabbitMqConnection } from "../infra/rabbitmq/rabbitmqConnection";
import { RabbitMqEventBus } from "../infra/rabbitmq/rabbitmqEventBus";
import { IEventBus } from "../domain/interfaces/IEventBus";
import getDecorators from "inversify-inject-decorators";
import { Types } from "./types";
import { SubscriptionManager } from "../infra/rabbitmq/subscriptionManager";

export class DependencyResolver {
    private static DIContainer: Container;

    private constructor() { }

    public static getInstance() : Container {

        if(!this.DIContainer){
            this.DIContainer = new Container();
            this.ConfigureContainer();
        }

        return this.DIContainer;
    }

    private static ConfigureContainer() {
        this.DIContainer.bind<IEventBus>(Types.IEventBus).to(RabbitMqEventBus).inSingletonScope();
        this.DIContainer.bind<SubscriptionManager>(SubscriptionManager).toSelf().inSingletonScope();
        this.DIContainer.bind<RabbitMqConnection>(RabbitMqConnection).toSelf().inSingletonScope();
    }
}