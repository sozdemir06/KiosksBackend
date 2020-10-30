import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PublicLogoService } from 'src/app/core/services/stores/public-logo-and-footer-text-store';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { IPublicLogo } from 'src/app/shared/models/IPublicLogo';

@Component({
  selector: 'app-public-logo',
  templateUrl: './public-logo.component.html',
  styleUrls: ['./public-logo.component.scss'],
})
export class PublicLogoComponent implements OnInit {
  roleForUpdate: string[] = ['Sudo'];
  roleForDelete: string[] = ['Sudo'];
  roleForCreate: string[] = ['Sudo'];

  constructor(
    public publicLogoStore: PublicLogoService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {}

  uploadResult(logo:IPublicLogo){
    this.publicLogoStore.create(logo);
  }
  // onUnConfirm(image: IPublicLogo) {
  //   const dialogRef = this.dialog.open(ConfirmDialogComponent, {
  //     width: '45rem',
  //     data: {
  //       message: 'Logo yayın dışında tutmak istiyormusunuz.?',
  //     },
  //   });
  //   dialogRef.afterClosed().subscribe((result) => {
  //     if (result) {
  //       const photo: IPublicLogo = {
  //         ...image,
  //         isMain: false,
  //       };
  //       this.publicLogoStore.update(photo);
  //     }
  //   });
  // }

  onConfirm(image: IPublicLogo) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Bu logoyu ana logo yapmak istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const photo: IPublicLogo = {
          ...image,
          isMain: true,
        };
        this.publicLogoStore.makeMainLogo(photo);
      }
    });
  }

  onDelete(image: IPublicLogo) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '45rem',
      data: {
        message: 'Logoyu silmek istiyormusunuz.?',
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.publicLogoStore.delete(image);
      }
    });
  }
}
