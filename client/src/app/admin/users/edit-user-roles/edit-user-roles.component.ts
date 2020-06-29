import { Component, OnInit, Input, Inject, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { RoleStore } from 'src/app/core/services/stores/role-store';
import { RoleParams } from 'src/app/shared/models/RoleParams';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { RoleCategorStore } from 'src/app/core/services/stores/role-category-store';
import { PageEvent } from '@angular/material/paginator';
import { IRole } from 'src/app/shared/models/IRole';
import { UserRoleStore } from 'src/app/core/services/stores/user-role-store';
import { IUserList } from 'src/app/shared/models/IUser';
import { fromEvent } from 'rxjs';
import { map, debounceTime, distinctUntilChanged, delay } from 'rxjs/operators';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-edit-user-roles',
  templateUrl: './edit-user-roles.component.html',
  styleUrls: ['./edit-user-roles.component.scss']
})
export class EditUserRolesComponent implements OnInit,AfterViewInit{
  displayedColumns: string[] = ['RoleName', 'Description', 'CategoryName',"Actions"];
  @Input() dataSource:IRole[];
  unSubscribeSearchInputEvent:any;

  user:IUserList;

  @ViewChild('searchInputt', { static: true }) Input: ElementRef;


  constructor(
    public roleStore: RoleStore,
    public roleCategoryStore: RoleCategorStore,
    public userRoleStore:UserRoleStore,
    @Inject(MAT_DIALOG_DATA) public data:any,
    private dialog:MatDialog
  ) {
    this.user=this.data?.user;
  }

  ngOnInit(): void {
    console.log(this.user);
    this.userRoleStore.getUserRoles(this.user.id);
  }

  onDelete(role:IRole){
    const dialogRef= this.dialog.open(ConfirmDialogComponent,{
       width:"45rem",
     });

     dialogRef.afterClosed().subscribe(result=>{
       if(result){
         this.userRoleStore.delete(this.user.id,role.id);
       }
     })
  }

  ngAfterViewInit(){

      this.unSubscribeSearchInputEvent = fromEvent<any>(
        this.Input.nativeElement,
        'keyup'
      )
        .pipe(
          map((event) => event.target.value),
          delay(0),
          debounceTime(400),
          distinctUntilChanged()
        )
        .subscribe((result) => {
          const params = this.roleStore.getroleParams();
          params.search = result;
          this.roleStore.setRoleParams(params);
          this.roleStore.onGetRoles();
        });
 
   
  }

  onPageChange(event: PageEvent) {
    const params = this.roleStore.getroleParams();
    params.pageIndex = event.pageIndex + 1;
    params.pageSize = event.pageSize;
    this.roleStore.setRoleParams(params);
    this.roleStore.onGetRoles();
  }



  onSelectRole(role:IRole){
      if(role)
      {
         this.userRoleStore.create(this.user.id,role.id);
      }
  }



  onReset() {
    const params = new RoleParams();
    this.roleStore.setRoleParams(params);
    this.roleStore.onGetRoles();
  }

  onFilters(event: any) {
    const params = this.roleStore.getroleParams();
    params.roleCategoryId = event?.id;
    this.roleStore.setRoleParams(params);
    this.roleStore.onGetRoles();
  }

}
