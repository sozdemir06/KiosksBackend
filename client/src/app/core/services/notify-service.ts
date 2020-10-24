
import { Injectable } from '@angular/core';
import { MatSnackBarConfig, MatSnackBar } from "@angular/material/snack-bar";
import { NotifyComponent } from '../notify/notify.component';

@Injectable({providedIn: 'root'})
export class NotifyService {
    config:MatSnackBarConfig;

    constructor(
        private snackBar:MatSnackBar
    ) {
        this.config={
            duration:6000,
            horizontalPosition:"right",
            verticalPosition:"bottom"
        };
     }


     notify(type:string,message:string){
        this.snackBar.openFromComponent(NotifyComponent,{
            data:{
                type:type,
                message:message
            },
            ...this.config
        })
     }


    
    
}