import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { BehaviorSubject, Observable, throwError, pipe, AsyncSubject } from 'rxjs';
import { IPagination } from 'src/app/shared/models/IPagination';
import { IVehicleAnnounceList } from 'src/app/shared/models/IVehicleAnnounceList';
import { map, catchError, tap } from 'rxjs/operators';
import { VehicleAnnounceParams } from 'src/app/shared/models/VehicleAnnounceParams';
import produce from 'immer';
import { IVehicleAnnounceDetail } from 'src/app/shared/models/IVehicleAnnounceDetail';
import { IVehicleAnnouncePhoto } from 'src/app/shared/models/IVehicleAnnouncePhoto';
import { IVehicleAnnounceSubScreen } from 'src/app/shared/models/IVehicleAnnounceSubScreen';

@Injectable({ providedIn: 'root' })
export class VehilceAnnounceStore {
  apiUrl: string = environment.apiUrl;
  vehicleAnnounceParamas = new VehicleAnnounceParams();
  private subject = new BehaviorSubject<IPagination<IVehicleAnnounceList>>(
    null
  );
  vehicleannounces$: Observable<
    IPagination<IVehicleAnnounceList>
  > = this.subject.asObservable();
  private detailSubject = new BehaviorSubject<IVehicleAnnounceDetail>(null);
  detail$: Observable<
    IVehicleAnnounceDetail
  > = this.detailSubject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {
    this.getList(this.vehicleAnnounceParamas);
    
  }

  private getList(announceparams: VehicleAnnounceParams) {
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
      .get<IPagination<IVehicleAnnounceList>>(this.apiUrl + 'vehicleannounce/')
      .pipe(
        map((announces) => announces),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((announces) => {
          this.subject.next(announces);
        })
      );
    this.loadingService.showLoaderUntilCompleted(list$).subscribe();
  }

  create(model: IVehicleAnnounceList) {
    const create$ = this.httpClient
      .post<IVehicleAnnounceList>(this.apiUrl + 'vehicleannounce/', model)
      .pipe(
        map((announces) => announces),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((announces) => {
          const updateSubject = produce(this.subject.getValue(), (draft) => {
            draft.data.push(announces);
          });
          this.subject.next(updateSubject);
          this.notifyService.notify('success', 'Yeni araç ilanı eklendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model: Partial<IVehicleAnnounceList>) {
    const update$ = this.httpClient
      .put<IVehicleAnnounceList>(this.apiUrl + 'vehicleannounce/', model)
      .pipe(
        map((announces) => announces),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((announces) => {
          const updatedSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id === announces.id);
            if (index != -1) {
              draft.data[index] = announces;
            }
          });
          this.subject.next(updatedSubject);
          this.notifyService.notify(
            'success',
            'Yeni araç ilanı güncellendi...'
          );
        })
      );
    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  getDetail(announceId: number) {
    this.detailSubject.next(null);//
    const detail$ = this.httpClient
      .get<IVehicleAnnounceDetail>(
        this.apiUrl + 'vehicleannounce/detail/' + announceId
      )
      .pipe(
        map((announce) => announce),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((announce) => {
          this.detailSubject.next(announce);
        })
      );
    this.loadingService.showLoaderUntilCompleted(detail$).subscribe();
  }

  publish(model: Partial<IVehicleAnnounceList>) {
    const publish$ = this.httpClient
      .put<IVehicleAnnounceList>(this.apiUrl + 'vehicleannounce/publish', model)
      .pipe(
        map((announce) => announce),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((announce) => {
          const updateSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id === announce.id);
            const update: IVehicleAnnounceList = {
              ...draft.data[index],
              ...announce,
            };
            draft.data[index] = update;
          });
          this.subject.next(updateSubject);
          this.notifyService.notify('success', 'İşlem Başarılı...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(publish$).subscribe();
  }

  addPhoto(announceId: number, photo: IVehicleAnnouncePhoto) {
    const updatePhoto = produce(this.detailSubject.getValue(), (draft) => {
      draft.vehicleAnnouncePhotos.push(photo);
    });
    this.detailSubject.next(updatePhoto);
    this.notifyService.notify('success', 'Resim Eklendi...');
  }

  updatePhoto(photo: IVehicleAnnouncePhoto) {
    const update$ = this.httpClient
      .put<IVehicleAnnouncePhoto>(this.apiUrl + 'vehicleannouncephotos', photo)
      .pipe(
        map((result) => result),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((result) => {
          const updateDetailsubject = produce(
            this.detailSubject.getValue(),
            (draft) => {
              const photoIndex = draft.vehicleAnnouncePhotos.findIndex(
                (x) => x.id == result.id
              );
              draft.vehicleAnnouncePhotos[photoIndex] = result;
            }
          );
          this.detailSubject.next(updateDetailsubject);
          this.notifyService.notify('success', 'Fotoğraf Güncellendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  deletePhoto(model: IVehicleAnnouncePhoto) {
    const deletePhoto$ = this.httpClient
      .delete<IVehicleAnnouncePhoto>(
        this.apiUrl + 'vehicleannouncephotos/' + model.id
      )
      .pipe(
        map((photo) => photo),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((photo) => {
          const updateDetailsubject = produce(
            this.detailSubject.getValue(),
            (draft) => {
              const photoIndex = draft.vehicleAnnouncePhotos.findIndex(
                (x) => x.id === photo.id
              );
              if (photoIndex != -1) {
                draft.vehicleAnnouncePhotos.splice(photoIndex, 1);
              }
            }
          );
          this.detailSubject.next(updateDetailsubject);
          this.notifyService.notify('success', 'Fotoğraf Silindi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(deletePhoto$).subscribe();
  }

  addSubScreen(model: Partial<IVehicleAnnounceSubScreen>) {
    const addSubScreen$ = this.httpClient
      .post<IVehicleAnnounceSubScreen>(
        this.apiUrl + 'vehicleannouncesubscreens',
        model
      )
      .pipe(
        map((subscreen) => subscreen),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((subscreen) => {
          const updateDetailsubject = produce(
            this.detailSubject.getValue(),
            (draft) => {
              draft.vehicleAnnounceSubScreens.push(subscreen);
            }
          );
          this.detailSubject.next(updateDetailsubject);
          this.notifyService.notify('success', 'Yayın Ekranı Eklendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(addSubScreen$).subscribe();
  }

  removeSubScreen(model:IVehicleAnnounceSubScreen){
    const removeSubScreen$=this.httpClient.delete<IVehicleAnnounceSubScreen>(this.apiUrl+"vehicleannouncesubscreens/"+model.id)
    .pipe(
      map((subscreen) => subscreen),
      catchError((error) => {
        this.notifyService.notify('error', error);
        return throwError(error);
      }),
      tap((subscreen) => {
        const updateDetailsubject = produce(
          this.detailSubject.getValue(),
          (draft) => {
            const index=draft.vehicleAnnounceSubScreens.findIndex(x=>x.id===subscreen.id);
            if(index!=-1){
               draft.vehicleAnnounceSubScreens.splice(index,1);
            }
          }
        );
        this.detailSubject.next(updateDetailsubject);
        this.notifyService.notify('success', 'Yayın Ekranı Kaldırıldı...');
      })
    );
    this.loadingService.showLoaderUntilCompleted(removeSubScreen$).subscribe();
  }
  getParams(): VehicleAnnounceParams {
    return this.vehicleAnnounceParamas;
  }

  setParams(params: VehicleAnnounceParams): void {
    this.vehicleAnnounceParamas = params;
  }

  getListByParams() {
    this.getList(this.vehicleAnnounceParamas);
  }
}
