import "reflect-metadata";
import { injectable, inject, multiInject } from "inversify";
import { DependencyResolver } from "./container/di-container";
import { IEventBus } from "./domain/interfaces/IEventBus";
import { Types } from "./container/types";
import { IEventHandler } from "./domain/interfaces/IEventHandler";
import { Event } from "./domain/entities/events/event";

const container = DependencyResolver.getInstance();

@injectable()
class Service {
  constructor(
    @inject(Types.IEventBus) private _eventBus: IEventBus,
    @multiInject(Types.IEventHandler) private handlers: IEventHandler<Event>[]
  ) {
    handlers.forEach((handler) =>
      _eventBus.subscribe(handler.queue(), handler)
    );
  }
}

container.resolve(Service);
