import { injectable } from "inversify";
import * as admin from "firebase-admin";
import firebase from '../../config/files/firebase-adminsdk.json'

@injectable()
export class FCMConnection {
  private instance: admin.app.App;

  constructor(){
      this.instance = this.getInstance();
  }

  public getInstance(): admin.app.App {
    if (!this.instance) {
      this.instance = this.createInstance();
    }

    return this.instance;
  }

  private createInstance() {
    return admin.initializeApp({
      credential: admin.credential.cert({
        clientEmail: firebase.client_email,
        privateKey: firebase.private_key,
        projectId: firebase.project_id,
      }),
    });
  }
}
