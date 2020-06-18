import {
  Component,
  OnInit,
} from '@angular/core';
import { UserStore } from 'src/app/core/services/user-store';
import { PageEvent } from '@angular/material/paginator';
import { IToolbarFilterList } from 'src/app/shared/models/toolbar-filter-list';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { UserEditDialogComponent } from './user-edit-dialog/user-edit-dialog.component';
import { LoadingService } from 'src/app/core/services/loading-service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
 
})
export class UsersComponent implements OnInit{

  toolbarTitle:string="Kullanıcı adı,soyadı,birim ve ünavana göre arama...";

  filters: IToolbarFilterList[] = [
    { id: 1, name: 'Aktif Kullanıcılar', description:"efefef" },
    { id: 2, name: 'Pasif Kullanıcılar', description: "bıdı bıdı" },
  ];

  constructor(
    public userService: UserStore,
    private dialog:MatDialog 
    ) {}

  ngOnInit(): void {
   
  }


  

  onSearch(eventData:string){
    const params = this.userService.getUserParams();
    params.search = eventData;
    params.pageIndex = 1;
    this.userService.onGetUsers();
  }

  onPageChange(event: PageEvent) {
    const params = this.userService.getUserParams();
    params.pageIndex = event.pageIndex + 1;
    params.pageSize=event.pageSize;
    this.userService.setUserParams(params);
    this.userService.onGetUsers();
  }

  onWaitingConfirm(event) {
    console.log(event);
  }

  onCreateNew() {
     const dialogRef=this.dialog.open(UserEditDialogComponent,{
       width:"45rem",
       maxHeight:"100vh",
       data:{
         title:"Yeni Kullanıcı Ekle",
         mode:"create"
       }
     });
  }

  filterBy(event: boolean) {}


}
