import { Component, OnInit } from '@angular/core';
import { RoleStore } from 'src/app/core/services/stores/role-store';
import { PageEvent } from '@angular/material/paginator';
import { RoleParams } from 'src/app/shared/models/RoleParams';
import { MatDialog } from '@angular/material/dialog';
import { EditRolesDialogComponent } from './edit-roles-dialog/edit-roles-dialog.component';
import { RoleCategorStore } from 'src/app/core/services/stores/role-category-store';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.scss'],
})
export class RolesComponent implements OnInit {
  toolbarTitle: string = 'Yetki Listesi';
  toolbarSearchInputPlaceholder: string =
    'Yetki adı,açıklama ve yetki kategori adına göre arama...';

  constructor(
    public roleStore: RoleStore,
    private dialog: MatDialog,
    public roleCategoryStore: RoleCategorStore
  ) {}

  ngOnInit(): void {}

  onPageChange(event: PageEvent) {
    const params = this.roleStore.getroleParams();
    params.pageIndex = event.pageIndex + 1;
    params.pageSize = event.pageSize;
    this.roleStore.setRoleParams(params);
    this.roleStore.onGetRoles();
  }

  onSearch(searchKeyWord: string) {
    const params = this.roleStore.getroleParams();
    params.search = searchKeyWord;
    this.roleStore.setRoleParams(params);
    this.roleStore.onGetRoles();
  }

  onCreate() {
    this.dialog.open(EditRolesDialogComponent, {
      width: '45rem',
      maxHeight: '100vh',
      data: {
        title: 'Yeni Yetki Ekle',
        mode: 'create',
        role: null,
      },
    });
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
