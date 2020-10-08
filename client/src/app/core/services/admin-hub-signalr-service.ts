import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { NotifyService } from './notify-service';
import { BehaviorSubject, Observable } from 'rxjs';
import { IUser } from 'src/app/shared/models/IUser';

@Injectable({providedIn: 'root'})
export class AdminHubService {
    hubUrl:string=environment.hubUrl;
    hubConnection:HubConnection;
    private statusSubject=new BehaviorSubject<string>(null);
    status$:Observable<string>=this.statusSubject.asObservable();

    constructor(
        private httpClient: HttpClient,
        private notifyService:NotifyService
        
    ) { }

    createHubConneciton(user:IUser){
        this.hubConnection=new HubConnectionBuilder()
                .withUrl(this.hubUrl+"AdminHub",{
                    accessTokenFactory:()=>user.token
                })
                .withAutomaticReconnect()
                .build();
        //start conneciton
        this.hubConnection
            .start()
            .then(()=>{
                this.statusSubject.next(this.hubConnection.state);
            })
            .catch(error=>{
               this.statusSubject.next(this.hubConnection.state);
            });

        //Catch Reconneciton State 
        this.hubConnection.onreconnected(()=>{
            this.statusSubject.next(this.hubConnection.state);
        });
        //Catch Reconneciton State End
        this.hubConnection.onreconnecting(()=>{
            this.statusSubject.next(this.hubConnection.state);
        })
        
    }


    onListeners(){

    }


    stopHubConnection(){
        this.hubConnection.stop().catch(error=>{
            this.statusSubject.next(this.hubConnection.state);
        })
    }
    
}