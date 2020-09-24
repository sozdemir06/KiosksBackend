import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PublicFoodMenuDetailComponent } from '../../details/public-food-menu-detail/public-food-menu-detail.component';
import { IFoodMenuForPublic } from '../../models/IFoodMenuForPublic';
import { PublicStore } from '../../store/public-store';

@Component({
  selector: 'app-public-foodmenu-list',
  templateUrl: './public-foodmenu-list.component.html',
  styleUrls: ['./public-foodmenu-list.component.scss'],
})
export class PublicFoodmenuListComponent implements OnInit {
  foodsmenu$: Observable<IFoodMenuForPublic[]>;

  constructor(
    private publicStore: PublicStore,
    private dialog:MatDialog
    ) {}

  ngOnInit(): void {
    this.foodsmenu$ = this.publicStore?.allannounces$.pipe(
      map((foodsmenu) => foodsmenu?.foodsMenu)
    );
  }

  onDetail(foodmenu:IFoodMenuForPublic){
    this.dialog.open(PublicFoodMenuDetailComponent,{
      width:"100vw",
      height:"90vh",
      data:{
        foodsmenu:foodmenu
      }
    })
  }
}
