import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IUserList } from 'src/app/shared/models/IUser';
import { IPagination } from 'src/app/shared/models/IPagination';
import { UserParams } from 'src/app/shared/models/UserParams';
import { environment } from 'src/environments/environment';
import { map, catchError, tap,delay } from 'rxjs/operators';
import { produce } from 'immer';
import { NotifyService } from '../notify-service';
import { LoadingService } from '../loading-service';
import { IUserPhoto } from 'src/app/shared/models/IUserPhoto';

@Injectable({ providedIn: 'root' })
export class UserStore {
  apiUrl = environment.apiUrl;

  private userSubject = new BehaviorSubject<IPagination<IUserList>>(null);
  users$: Observable<IPagination<IUserList>> = this.userSubject.asObservable();

  userParams = new UserParams();

  constructor(
    private httpClient: HttpClient,
    private notificationService: NotifyService,
    private loadingService: LoadingService,
  ) {
    this.getUsers(this.userParams);
  }

  private getUsers(userParams: UserParams) {
    let params = new HttpParams();

    if (userParams.search) {
      params = params.append('search', userParams.search);
    }
    if (userParams.sort) {
      params = params.append('sort', userParams.sort);
    }
    if (userParams.statusPassive) {
      params = params.append('statusPassive', userParams.statusPassive);
    }

    if (userParams.statusActive) {
      params = params.append('statusActive', userParams.statusActive);
    }
    params = params.append('pageIndex', userParams.pageIndex.toString());
    params = params.append('pageSize', userParams.pageSize.toString());

    const userList$ = this.httpClient
      .get<IPagination<IUserList>>(this.apiUrl + 'users', { params })
      .pipe(
        map((result) => result),
        catchError((error) => {
          this.notificationService.notify('error', error);
          return throwError(error);
        }),
        tap((users) => this.userSubject.next(users))
      );
    this.loadingService.showLoaderUntilCompleted(userList$).subscribe();
  }

  create(model: IUserList) {
    const create$ = this.httpClient
      .post<IUserList>(this.apiUrl + 'auth/register', model)
      .pipe(
        map((user) => user),
        catchError((error) => {
          this.notificationService.notify('error', error);
          return throwError(error);
        }),
        tap((user) => {
          const newUser = produce(this.userSubject.getValue(), (draft) => {
            draft.data.push(user);
          });
          this.userSubject.next(newUser);
          this.notificationService.notify(
            'success',
            'Kayıt işlemi tamamlandı.profil bilgileriniz onaylandıktan sonra giriş yapabilirsiniz.'
          );
        })
      );

    this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }

  update(userId: number, changes: Partial<IUserList>) {
    const update$ = this.httpClient
      .put<IUserList>(this.apiUrl + 'users', changes)
      .pipe(
        delay(1000),
        map((user) => user),
        catchError((error) => {
          this.notificationService.notify('error', error);
          return throwError(error);
        }),
        tap((user) => {
          const updatedUser = produce(this.userSubject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id == userId);
            const updateUser: IUserList = {
              ...draft.data[index],
              ...user,
            };
            draft.data[index] = updateUser;
          });

          this.userSubject.next(updatedUser);
          this.notificationService.notify('success', 'Kullanıcı Güncellendi.');
        })
      );

    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

 getuserById(userId:number):Observable<IUserList>{
   return this.users$.pipe(
     map(users=>users?.data?.find(x=>x.id==userId))
   );
 }
  getUnConfirmUserPhoto(): Observable<number> {
    return this.users$.pipe(
      map(
        (users) =>
          users?.data?.filter((x) =>
            x.userPhotos.find((x) => x.isConfirm === false)
          )?.length
      )
    );
  }

  addPhoto(photo: IUserPhoto) {
    const createphoto = produce(this.userSubject.getValue(), (draft) => {
      const index = draft.data.findIndex((x) => x.id == photo.userId);
      if (index! - 1) {
        draft.data[index].userPhotos.push(photo);
      }
    });
    this.userSubject.next(createphoto);
    this.notificationService.notify('success', 'Fotoğraf Eklendi...');
  }

  updatePhoto(photo: IUserPhoto) {
    const update$ = this.httpClient
      .put<IUserPhoto>(this.apiUrl + 'userphoto', photo)
      .pipe(
        map((userphoto) => userphoto),
        catchError((error) => {
          this.notificationService.notify('error', error);
          return throwError(error);
        }),
        tap((userphoto) => {
          const updatephoto = produce(this.userSubject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id == userphoto.userId);
            if (index != -1) {
              const photoIndex = draft.data[index].userPhotos.findIndex(
                (x) => x.id == userphoto.id
              );
              if (photoIndex != -1) {
                draft.data[index].userPhotos[photoIndex] = userphoto;
              }
            }
          });
          this.userSubject.next(updatephoto);
          this.notificationService.notify('success', 'Fotoğraf Güncellendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  deletePhoto(photo: IUserPhoto) {
    const delete$ = this.httpClient
      .delete<IUserPhoto>(this.apiUrl + 'userphoto/' + photo.id)
      .pipe(
        map((userphoto) => userphoto),
        catchError((error) => {
          this.notificationService.notify('error', error);
          return throwError(error);
        }),
        tap((userphoto) => {
          const updatephoto = produce(this.userSubject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id == userphoto.userId);
            if (index != -1) {
              const photoIndex = draft.data[index].userPhotos.findIndex(
                (x) => x.id == userphoto.id
              );
              if (photoIndex != -1) {
                draft.data[index].userPhotos.splice(photoIndex, 1);
              }
            }
          });
          this.userSubject.next(updatephoto);
          this.notificationService.notify('success', 'Fotoğraf Güncellendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(delete$).subscribe();
  }

  getNewAnnounceLength(): Observable<number> {
    return this.users$.pipe(
      map((announces) => {
        const newanouncecount = announces?.data.filter(
          (x) => x.isActive == false
        ).length;
        let photoCount = 0;
        announces?.data?.forEach((x) => {
          x.userPhotos.forEach((x) => {
            if (x.isConfirm == false && x.unConfirm == false) {
              photoCount++;
            }
          });
        });
        return newanouncecount + photoCount;
      })
    );
  }

  getUserParams(): UserParams {
    return this.userParams;
  }

  setUserParams(userParams: UserParams) {
    this.userParams = userParams;
  }

  onGetUsers() {
    this.getUsers(this.userParams);
  }
}
