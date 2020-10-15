import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class RealtimeNotificationService {
  public message$ = new Subject<string>();
  private connection: signalR.HubConnection;

  constructor() {
    this.connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7890/notificationhub").build();
    this.setupConnection();  
   }
  
  public setupConnection(): void {
    this.connection.start().then(() => {
      this.discountNotificationListener();
      // Call hub method from client
      this.connection.invoke("DiscountNotification").then(() => console.log(`Hub method is called from the client`))
      .catch((err) => console.log(`${err}`));
    }).catch((err) => {
      console.log(`Connection failed error is ${err.message}`);
      // retry after 5s if failed
      setTimeout(() => {
        this.setupConnection()
      }, 5000);
    });
  }

  public discountNotificationListener(): void {
    // Call client method from the hub
    this.connection.on("discountNotification", (msg: string) => {
      console.log(`Client method is called from the hub`);
      this.message$.next(msg);
    });
  }


}
