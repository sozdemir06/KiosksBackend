import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FoodMenuStore } from 'src/app/core/services/stores/food-menu-store';
import { Location } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { IFoodMenuPhoto } from 'src/app/shared/models/IFoodMenuPhoto';
import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { IFoodMenuSubScreen } from 'src/app/shared/models/IFoodMenuSubScreen';

@Component({
  selector: 'app-food-menu-detail',
  templateUrl: './food-menu-detail.component.html',
  styleUrls: ['./food-menu-detail.component.scss']
})
export class FoodMenuDetailComponent implements OnInit {
  announceId: number;
  roleForAddPhoto: string[] = [
    'Sudo',
    'FoodMenu.Create',
    'FoodMenu.All',
  ];
  roleForUpdatePhoto: string[] = [
    'Sudo',
    'FoodMenu.Update',
    'FoodMenu.All',
  ];
  roleForDeletePhoto: string[] = [
    'Sudo',
    'FoodMenu.Delete',
    'FoodMenu.All',
  ];

  roleForAddSubScreen: string[] = [
    'Sudo',
    'FoodMenu.Create',
    'FoodMenu.All',
  ];


  constructor(
    private route: ActivatedRoute,
    public foodMenuStore: FoodMenuStore,
    private location: Location,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.announceId = +this.route.snapshot.paramMap.get('id');
    this.foodMenuStore.getDetail(this.announceId);
  }

  goBack() {
    this.location.back();
  }

  uploadResult(model: IFoodMenuPhoto) {
    this.foodMenuStore.addPhoto(model);
  }

  onSelectSubScreen(subscreen: ISubScreen) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: `Menüyü  ${subscreen.name} adlı ekranda yayınlamak istiyormusunuz?`,
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: Partial<IFoodMenuSubScreen> = {
          foodMenuId: this.announceId,
          subScreenId: subscreen?.id,
          screenId: subscreen?.screenId,
        };
        this.foodMenuStore.addSubScreen(model);
      }
    });
  }
}
