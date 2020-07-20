import { v4 as uuid } from 'uuid';

export abstract class Event {
    private _createdAt : Date;
    private _id : string;

    protected constructor() {
        this._createdAt = new Date();
        this._id = uuid();
    }

    public getId() {
        return this._id;
    }

    public getCreatedAt() {
        return this._createdAt;
    }

    abstract getType() : string;
}