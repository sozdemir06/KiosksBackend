import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IPagination } from 'src/app/shared/models/IPagination';
import { LoadingService } from 'src/app/core/services/loading-service';
import { NotifyService } from 'src/app/core/services/notify-service';
import { IUser } from 'src/app/shared/models/IUser';
import { catchError, map, tap } from 'rxjs/operators';
import { IVehicleAnnounceForPublic } from '../models/IVehicleAnnounceForPublic';
import { VehicleAnnounceParams } from 'src/app/shared/models/VehicleAnnounceParams';
import produce from 'immer';
import { IVehicleAnnouncePhoto } from 'src/app/shared/models/IVehicleAnnouncePhoto';

const AUTH_DATA = 'auth_data';

@Injectable({ providedIn: 'root' })
export class UserVehicleAnnounceStore {
  apiUrl: string = environment.apiUrl;
  private subject = new BehaviorSubject<IPagination<IVehicleAnnounceForPublic>>(
    null
  );
  Vehicleannounces$: Observable<
    IPagination<IVehicleAnnounceForPublic>
  > = this.subject.asObservable();

  vehicleAnnounceParams = new VehicleAnnounceParams();
  user: IUser;

  constructor(
    private httpClient: HttpClient,
    private laodingService: LoadingService,
    private notifyService: NotifyService
  ) {
    const user: IUser = JSON.parse(localStorage.getItem(AUTH_DATA));
    if (user) {
      this.getList(user, this.vehicleAnnounceParams);
      this.user = user;
    }
  }

  private getList(user: IUser, announceparams: VehicleAnnounceParams) {
    let params = new HttpParams();
    if (announceparams.search) {
      params = params.append('search', announceparams.search);
    }
    if (announceparams.sort) {
      params = params.append('sort', announceparams.sort);
    }
    if (announceparams.screenId) {
      params = params.append('screenId', announceparams.screenId.toString());
    }
    if (announceparams.subScreenId) {
      params = params.append(
        'subScreenId',
        announceparams.subScreenId.toString()
      );
    }
    if (announceparams.isNew) {
      params = params.append('isNew', announceparams.isNew.toString());
    }
    if (announceparams.isPublish) {
      params = params.append('isPublish', announceparams.isPublish.toString());
    }
    if (announceparams.reject) {
      params = params.append('reject', announceparams.reject.toString());
    }

    params = params.append('pageIndex', announceparams.pageIndex.toString());
    params = params.append('pageSize', announceparams.pageSize.toString());
    const list$ = this.httpClient
      .get<IPagination<IVehicleAnnounceForPublic>>(
        this.apiUrl + 'PublicUserAnnounce/Vehicleannounces/' + user.userId,
        { params }
      )
      .pipe(
        map((Vehicleannounces) => Vehicleannounces),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((Vehicleannounces) => {
          this.subject.next(Vehicleannounces);
        })
      );
    this.laodingService.showLoaderUntilCompleted(list$).subscribe();
  }

  create(model: IVehicleAnnounceForPublic, userId: number) {
    const create$ = this.httpClient
      .post<IVehicleAnnounceForPublic>(
        this.apiUrl + 'vehicleannounce/createforuser/' + userId,
        model
      )
      .pipe(
        map((vehicleannounce) => vehicleannounce),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((vehicleannounce) => {
          const updateSubject = produce(this.subject.getValue(), (draft) => {
            draft.data.push(vehicleannounce);
          });
          this.subject.next(updateSubject);
          this.notifyService.notify('success', 'Yeni Araç ilanı eklendi...');
        })
      );
    this.laodingService.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model: IVehicleAnnounceForPublic, userId: number) {
    const update$ = this.httpClient
      .put<IVehicleAnnounceForPublic>(
        this.apiUrl + 'vehicleannounce/updateforuser/' + userId,
        model
      )
      .pipe(
        map((vehicleannounce) => vehicleannounce),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((vehicleannounce) => {
          const updateSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id === vehicleannounce.id);
            if (index != -1) {
              draft.data[index] = vehicleannounce;
            }
          });
          this.subject.next(updateSubject);
          this.notifyService.notify('success', 'Araç ilanı güncellendi...');
        })
      );
    this.laodingService.showLoaderUntilCompleted(update$).subscribe();
  }

  addPhoto(model: IVehicleAnnouncePhoto, announceId: number) {
    const updateSubject = produce(this.subject.getValue(), (draft) => {
      const index = draft.data.findIndex((x) => x.id === announceId);
      if (index != -1) {
        draft.data[index].vehicleAnnouncePhotos.push(model);
      }
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success', 'Fotoğraf Eklendi...');
  }

  getParams(): VehicleAnnounceParams {
    return this.vehicleAnnounceParams;
  }
  setParams(params: VehicleAnnounceParams): void {
    this.vehicleAnnounceParams = params;
  }
  getListByParams(): void {
    this.getList(this.user, this.vehicleAnnounceParams);
  }
}
