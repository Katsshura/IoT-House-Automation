export class PushNotification {
  constructor(
    private _title: string,
    private _body: string,
    private _icon: string | undefined,
    private _onClick: string | undefined,
    private _token : string,
    private _pushType: string
  ) {}

  get title() {
    return this._title;
  }

  get body() {
      return this._body;
  }

  get icon() {
      return this._icon;
  }

  get onClick() {
      return this._onClick;
  }

  get token() {
      return this._token;
  }

  get pushType() {
      return this._pushType;
  }
}
