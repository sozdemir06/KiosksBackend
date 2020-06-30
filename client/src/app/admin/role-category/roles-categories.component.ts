import { Component, OnInit } from '@angular/core';
import { RoleCategorStore } from 'src/app/core/services/stores/role-category-store';
import { MatDialog } from '@angular/material/dialog';
import { EditRoleCategoryDialogComponent } from './edit-role-category-dialog/edit-role-category-dialog.component';

@Component({
  selector: 'app-roles-categories',
  templateUrl: './roles-categories.component.html',
  styleUrls: ['./roles-categories.component.scss']
})
export class RolesCategoriesComponent implements OnInit {
toolbarTitle:string="Yetki Kategori Listesi";
toolbarSerachInputPlaceHolder:string="Role Kategori için arama yok";
allowedRoles:string[]=["Sudo"];
  constructor(
    public roleCategoryStore:RoleCategorStore,
    private dialog:MatDialog
  ) { }

  ngOnInit(): void {
  }

  onCreate(){
    this.dialog.open(EditRoleCategoryDialogComponent,{
      width:"45rem",
      maxHeight:"100vh",
      data:{
        title:"Yeni Yetki Ekle",
        mode:"create",
        roleCategory:null
      }
    })
  }

}
