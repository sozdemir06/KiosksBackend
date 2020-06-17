

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatDialogRef } from '@angular/material/dialog';
import { UserEditDialogComponent } from 'src/app/admin/users/user-edit-dialog/user-edit-dialog.component';
import { BehaviorSubject, Observable } from 'rxjs';
import { ICampus } from 'src/app/shared/models/ICampus';

@Injectable({providedIn: 'root'})
export class CampusStore {

    private subject=new BehaviorSubject<ICampus[]>([]);
    private loadingSubject=new BehaviorSubject<boolean>(false);

    campus$:Observable<ICampus[]>=this.subject.asObservable();
    loading$:Observable<boolean>=this.loadingSubject.asObservable();

    constructor(
        private httpClient: HttpClient,
        private dialogRef:MatDialogRef<UserEditDialogComponent>
        
    ) { }


    getList(){
            

    }


    
}