import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import produce from 'immer';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { LoadingService } from 'src/app/core/services/loading-service';
import { NotifyService } from 'src/app/core/services/notify-service';
import { IUser, IUserList } from 'src/app/shared/models/IUser';
import { IUserPhoto } from 'src/app/shared/models/IUserPhoto';
import { environment } from 'src/environments/environment';
import { IUserCamPusAndDepartmentAndDegree } from '../models/IUserCamPusAndDepartmentAndDegree';

const AUTH_DATA = 'auth_data';

@Injectable({ providedIn: 'root' })
export class PublicUserStore {
  apiUrl: string = environment.apiUrl;

  private subject = new BehaviorSubject<IUserList>(null);
  user$: Observable<IUserList> = this.subject.asObservable();

  constructor(
    private loadingService: LoadingService,
    private notifyService: NotifyService,
    private httpClient: HttpClient,
    private router: Router
  ) {
    this.getUser();
  }

  private getUser() {
    const user: IUser = JSON.parse(localStorage.getItem(AUTH_DATA));
    if (!user) {
      this.notifyService.notify(
        'error',
        'Profil sayfası için önce giriş yapmalısınız.'
      );
      this.router.navigateByUrl('/app/home');
      return false;
    }

    const user$ = this.httpClient
      .get<IUserList>(this.apiUrl + 'public/userbyId/' + user.userId)
      .pipe(
        map((userr) => userr),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((userr) => {
          this.subject.next(userr);
        })
      );
    this.loadingService.showLoaderUntilCompleted(user$).subscribe();
  }

  updateUser(user: Partial<IUserList>, userId: number) {
    const update$ = this.httpClient
      .put<IUserList>(this.apiUrl + 'public/updateuser/' + userId, user)
      .pipe(
        map((userdata) => userdata),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((userdata) => {
          const updatesubject = produce(this.subject.getValue(), (draft) => {
            draft.interPhone = userdata.interPhone;
            draft.gsmPhone = userdata.gsmPhone;
            draft.campus = userdata.campus;
            draft.department = userdata.department;
          });
          this.subject.next(updatesubject);
          this.notifyService.notify(
            'success',
            'Profil bilgileriniz güncellendi..'
          );
        })
      );
    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  changePassword(model: any, userId: number) {
    const changePassword$ = this.httpClient
      .post(this.apiUrl + 'public/changepassword/' + userId, model)
      .pipe(
        map((result) => result),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap(() => {
          this.notifyService.notify('success', 'Şifreniz değiştirilsi...');
        })
      );

    this.loadingService.showLoaderUntilCompleted(changePassword$).subscribe();
  }

  uploadNewPhoto(photo: IUserPhoto) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      draft.userPhotos.push(photo);
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success', 'Resim Eklendi...');
  }

  makeMainPhoto(photo: IUserPhoto, userId: number) {
    const update$ = this.httpClient
      .put<IUserPhoto>(this.apiUrl + 'public/makemainphoto/' + userId, photo)
      .pipe(
        map((photos) => photos),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((photos) => {
          const updateSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.userPhotos.findIndex((x) => x.id == photos.id);
            const indexIsMain=draft.userPhotos.findIndex(x=>x.isMain==true);
            if(indexIsMain!=-1){
                draft.userPhotos[indexIsMain].isMain=false;
            }
            if (index != -1) {
              draft.userPhotos[index] = photos;
            }
          });
          this.subject.next(updateSubject);
          this.notifyService.notify('success', 'Ana Resim Seçildi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  getUserCamPusAndDepartmentAndDegree() {
    return this.httpClient.get<IUserCamPusAndDepartmentAndDegree>(
      this.apiUrl + 'public/UserCamPusAndDepartmentAndDegree'
    );
  }
}
