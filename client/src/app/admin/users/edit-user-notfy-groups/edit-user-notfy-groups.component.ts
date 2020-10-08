import { AfterViewInit, Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { UserNotifyStore } from 'src/app/core/services/stores/user-notify-store';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { INotifyGroup } from 'src/app/shared/models/INotifyGroup';
import { IUserList } from 'src/app/shared/models/IUser';
import { IUserNotifyGroup } from 'src/app/shared/models/IUserNotifyGroup';

@Component({
  selector: 'app-edit-user-notfy-groups',
  templateUrl: './edit-user-notfy-groups.component.html',
  styleUrls: ['./edit-user-notfy-groups.component.scss'],
})
export class EditUserNotfyGroupsComponent implements OnInit, AfterViewInit {
  user: IUserList;
  displayedColumns: string[] = ['Id', 'Description', 'Actions'];
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public userNotifyStore: UserNotifyStore,
    private dialog: MatDialog
  ) {
    this.user = data?.user;
  }

  ngAfterViewInit() {
    setTimeout(() => {
      this.userNotifyStore.getUserNotifyGroup(this.user?.id);
    });
  }

  ngOnInit(): void {}

  onAddNotifyGroup(notifyGroup: INotifyGroup) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '60rem',
      maxHeight: '90vh',
      data: {
        message: `${notifyGroup.description}  grubunu atamak istiyormusunuz.?`,
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        const model: Partial<IUserNotifyGroup> = {
          userId: this.user?.id,
          notifyGroupId: notifyGroup?.id,
        };

        this.userNotifyStore.addNewNotifyGroupForUser(model);
      }
    });
  }

  onRemoveNotifyGroup(userNotifyGroup: IUserNotifyGroup) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '60rem',
      maxHeight: '90vh',
      data: {
        message: `${userNotifyGroup.description} grubundan kaldırmak istiyormusunuz.?`,
      },
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.userNotifyStore.removeNotifyGroupForUser(userNotifyGroup);
      }
    });
  }
}
