import { Component, OnInit } from '@angular/core';
import { ScreenStore } from 'src/app/core/services/stores/screen-store';
import { IScreen } from 'src/app/shared/models/IScreen';
import { MatDialog } from '@angular/material/dialog';
import { EditScreensDialogComponent } from '../edit-screens-dialog/edit-screens-dialog.component';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { EditScreenHeaderComponent } from '../edit-screen-header/edit-screen-header.component';
import { EditScreenFooterComponent } from '../edit-screen-footer/edit-screen-footer.component';
import { EditScreenHeaderPhotoComponent } from '../edit-screen-header-photo/edit-screen-header-photo.component';
import { IScreenHeader } from 'src/app/shared/models/IScreenHeader';

@Component({
  selector: 'app-screens-list',
  templateUrl: './screens-list.component.html',
  styleUrls: ['./screens-list.component.scss'],
})
export class ScreensListComponent implements OnInit {
  panelOpenState: boolean = false;
  displayedColumns: string[] = ['Id', 'Name', 'Position', 'IsFull', 'Actions'];
  allowedRoleForUpdate: string[] = ['Sudo', 'Screens.Update','Screens.All'];


  constructor(public screenStore: ScreenStore, private dialog: MatDialog) {}

  ngOnInit(): void {}

  onUpdate(element: IScreen) {
    this.dialog.open(EditScreensDialogComponent, {
      width: '45rem',
      maxHeight: '100vh',
      data: {
        title: 'Ekran bilgilerini güncelle',
        mode: 'update',
        item: element,
      },
    });
  }

  onDelete(element: IScreen) {
    // const dialogRef = this.dialog.open(ConfirmDialogComponent, {
    //   width: '45rem',
    // });

    // dialogRef.afterClosed().subscribe((result) => {
    //   if (result) {
    //     this.screenStore.delete(element);
    //   }
    // });
  }



  onCreateHeader(element:IScreen){
      this.dialog.open(EditScreenHeaderComponent,{
        width:"45rem",
        maxHeight:"100vh",
        data:{
          title:"Ekran Üst Başlığı Ekle",
          item:element,
          mode:"create"
        }
      })
  }
  onUpdateHeader(element:IScreen){
    this.dialog.open(EditScreenHeaderComponent,{
      width:"45rem",
      maxHeight:"100vh",
      data:{
        title:"Ekran Üst Başlığı Güncelle",
        item:element,
        mode:"update"
      }
    })
}
  onCreateFooter(element:IScreen){
    this.dialog.open(EditScreenFooterComponent,{
      width:"45rem",
      maxHeight:"100vh",
      data:{
        title:"Ekran Alt Başlık ekle",
        item:element,
        mode:"create"
      }
    })
  }
  onUpdateFooter(element:IScreen){
    this.dialog.open(EditScreenFooterComponent,{
      width:"45rem",
      maxHeight:"100vh",
      data:{
        title:"Ekran Alt Başlığı Güncelle",
        item:element,
        mode:"update"
      }
    })
  }
  
  onHeaderPhoto(element:IScreen){
    this.dialog.open(EditScreenHeaderPhotoComponent,{
      width:"70vw",
      maxHeight:"100vh",
      data:{
        title:"Ekran Üst Başlık Sol-Logo ve Sağ-Logo Ekle",
        item:element
      }
    })
  }
}
