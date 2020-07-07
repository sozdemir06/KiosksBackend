import { Component, OnInit, Input } from '@angular/core';
import { IVehicleCategory } from 'src/app/shared/models/IVehicleCategory';
import { MatDialog } from '@angular/material/dialog';
import { VehicleCategoryStore } from 'src/app/core/services/stores/vehicle-category-store';
import { EditVehicleCategoryDialogComponent } from '../edit-vehicle-category-dialog/edit-vehicle-category-dialog.component';
import { EditRoleCategoryDialogComponent } from 'src/app/admin/role-category/edit-role-category-dialog/edit-role-category-dialog.component';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-vehicle-category-list',
  templateUrl: './vehicle-category-list.component.html',
  styleUrls: ['./vehicle-category-list.component.scss'],
})
export class VehicleCategoryListComponent implements OnInit {
  displayedColumns: string[] = ['Id', 'Name', 'Actions'];
  @Input() dataSource: IVehicleCategory[];
 allowedRolesVehicleCatgeoriesForUpdate:string[]=['Sudo','VehicleCategories.Update'];
 allowedRolesVehicleCatgeoriesForDelete:string[]=['Sudo','VehicleCategories.Delete'];
 
  constructor(
    private dialog: MatDialog,
    private vehicleCategoryStore: VehicleCategoryStore
  ) {}

  ngOnInit(): void {}

  onUpdate(element: IVehicleCategory) {
    this.dialog.open(EditVehicleCategoryDialogComponent, {
      width: '45rem',
      maxHeight: '100vh',
      data: {
        title: 'Ketagori Güncelle',
        mode: 'update',
        item: element,
      },
    });
  }

  onDelete(element: IVehicleCategory) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.vehicleCategoryStore.delete(element);
      }
    });
  }
}
