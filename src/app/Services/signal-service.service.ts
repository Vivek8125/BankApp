import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from '../Environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SignalServiceService {
  localToken: string = ''
  hubConnection ! : signalR.HubConnection;
  NotificationMessage :string = '';
  notifyHubUrl: string = `${environment.apiBaseUrl}${environment.apiUrls.notifyHub}`;

  constructor(private http: HttpClient) { }

  buildConnection(){
    this.localToken = JSON.parse(localStorage.getItem('localToken')!);
    this.hubConnection = new signalR.HubConnectionBuilder().withUrl(`${environment.apiBaseUrl}${environment.apiUrls.notifyHub}` ,{
      accessTokenFactory: () => {
        return this.localToken;
      }}).build()
  } 

  startConnection(){
    return this.hubConnection.start().then(()=>{this.getNotification(); console.log("SignalR connection Started");}).catch(er => console.log(er))
  }

  getNotification(){
    this.hubConnection.on("RecievedMessage", message => {
      this.NotificationMessage = message; 
    })
  }
}
