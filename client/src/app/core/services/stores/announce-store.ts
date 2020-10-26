import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { AnnounceParams } from 'src/app/shared/models/AnnounceParams';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IAnnounce } from 'src/app/shared/models/IAnnounce';
import { IPagination } from 'src/app/shared/models/IPagination';
import { map, catchError, tap } from 'rxjs/operators';
import produce from 'immer';
import { IAnnouncePhoto } from 'src/app/shared/models/IAnnouncePhoto';
import { IAnnounceSubScreen } from 'src/app/shared/models/IAnnounceSubScreen';


@Injectable({ providedIn: 'root' })
export class AnnounceStore {
  apiUrl: string = environment.apiUrl;
  announceParams = new AnnounceParams();

  private subject = new BehaviorSubject<IPagination<IAnnounce>>(null);
  announces$: Observable<IPagination<IAnnounce>> = this.subject.asObservable();
  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {
    this.getList(this.announceParams);
  }

  private getList(announceParams: AnnounceParams) {
    let params = new HttpParams();
    if (announceParams.search) {
      params = params.append('search', announceParams.search);
    }
    if (announceParams.sort) {
      params = params.append('sort', announceParams.sort);
    }
    if (announceParams.screenId) {
      params = params.append('screenId', announceParams.screenId.toString());
    }
    if (announceParams.subScreenId) {
      params = params.append(
        'subScreenId',
        announceParams.subScreenId.toString()
      );
    }
    if (announceParams.isNew) {
      params = params.append('isNew', announceParams.isNew.toString());
    }
    if (announceParams.isPublish) {
      params = params.append('isPublish', announceParams.isPublish.toString());
    }
    if (announceParams.reject) {
      params = params.append('reject', announceParams.reject.toString());
    }

    params = params.append('pageIndex', announceParams.pageIndex.toString());
    params = params.append('pageSize', announceParams.pageSize.toString());

    const list$ = this.httpClient
      .get<IPagination<IAnnounce>>(this.apiUrl + 'announces', { params })
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

  create(model: IAnnounce) {
    const create$ = this.httpClient
      .post<IAnnounce>(this.apiUrl + 'announces', model)
      .pipe(
        map((announce) => announce),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((announce) => {
          const createSubject = produce(this.subject.getValue(), (draft) => {
            draft.data.push(announce);
          });
          this.subject.next(createSubject);
          this.notifyService.notify('success', 'Yeni Duyuru Eklendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model: Partial<IAnnounce>) {
    const update$ = this.httpClient
      .put<IAnnounce>(this.apiUrl + 'announces', model)
      .pipe(
        map((announce) => announce),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((announce) => {
          const updateSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id == announce.id);
            if (index != -1) {
              draft.data[index] = announce;
            }
          });
          this.subject.next(updateSubject);
          this.notifyService.notify('success', 'Duyuru Güncellendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  delete(model: IAnnounce) {
    const delete$ = this.httpClient
      .delete<IAnnounce>(this.apiUrl + 'announces/' + model.id)
      .pipe(
        map((announce) => announce),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((announce) => {
          const deleteSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id == announce.id);
            if (index != -1) {
              draft.data.slice(index, 1);
            }
          });
          this.subject.next(deleteSubject);
          this.notifyService.notify('success', 'Duyuru silindi...');
        })
      );
  }

  getDetailById(announceId: number): Observable<IAnnounce> {
    return this.announces$.pipe(
      map((announces) => announces?.data.find((x) => x.id == announceId))
    );
  }

  publish(model: Partial<IAnnounce>) {
    const publish$ = this.httpClient
      .put<IAnnounce>(this.apiUrl + 'announces/publish', model)
      .pipe(
        map((announce) => announce),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((announce) => {
          const updateSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id === announce.id);
            const update: IAnnounce = {
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

  addPhoto(photo: IAnnouncePhoto) {
    const updatePhoto = produce(this.subject.getValue(), (draft) => {
      const index = draft.data.findIndex((x) => x.id === photo.announceId);
      if (index != -1) {
        draft.data[index].announcePhotos.push(photo);
      }
    });

    this.subject.next(updatePhoto);
    this.notifyService.notify('success', 'Dosya Eklendi...');
  }

  updatePhoto(photo: IAnnouncePhoto) {
    const update$ = this.httpClient
      .put<IAnnouncePhoto>(this.apiUrl + 'announcephotos', photo)
      .pipe(
        map((result) => result),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((result) => {
          const updateDetailsubject = produce(
            this.subject.getValue(),
            (draft) => {
              const index = draft.data.findIndex(
                (x) => x.id === result.announceId
              );
              if (index != -1) {
                const photoIndex = draft.data[index].announcePhotos.findIndex(
                  (x) => x.id == result.id
                );
                if (photoIndex != -1) {
                  draft.data[index].announcePhotos[photoIndex] = result;
                }
              }
            }
          );
          this.subject.next(updateDetailsubject);
          this.notifyService.notify('success', 'Dosya Güncellendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  deletePhoto(model: IAnnouncePhoto) {
    const deletePhoto$ = this.httpClient
      .delete<IAnnouncePhoto>(this.apiUrl + 'announcephotos/' + model.id)
      .pipe(
        map((photo) => photo),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((photo) => {
          const updateDetailsubject = produce(
            this.subject.getValue(),
            (draft) => {
              const index = draft.data.findIndex(
                (x) => x.id === photo.announceId
              );
              if (index != -1) {
                const photoIndex = draft.data[index].announcePhotos.findIndex(
                  (x) => x.id == photo.id
                );
                if (photoIndex != -1) {
                  draft.data[index].announcePhotos.splice(photoIndex, 1);
                }
              }
            }
          );
          this.subject.next(updateDetailsubject);
          this.notifyService.notify('success', 'Dosya Silindi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(deletePhoto$).subscribe();
  }

  addSubScreen(model: Partial<IAnnounceSubScreen>) {
    const addSubScreen$ = this.httpClient
      .post<IAnnounceSubScreen>(this.apiUrl + 'AnnounceSubScreens', model)
      .pipe(
        map((subscreen) => subscreen),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((subscreen) => {
          const updateDetailsubject = produce(
            this.subject.getValue(),
            (draft) => {
              const index = draft.data.findIndex(
                (x) => x.id === subscreen.announceId
              );
              if (index != -1) {
                draft.data[index].announceSubScreens.push(subscreen);
              }
            }
          );
          this.subject.next(updateDetailsubject);
          this.notifyService.notify('success', 'Yayın Ekranı Eklendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(addSubScreen$).subscribe();
  }

  removeSubScreen(model: IAnnounceSubScreen) {
    const removeSubScreen$ = this.httpClient
      .delete<IAnnounceSubScreen>(
        this.apiUrl + 'AnnounceSubScreens/' + model.id
      )
      .pipe(
        map((subscreen) => subscreen),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((subscreen) => {
          const updateDetailsubject = produce(
            this.subject.getValue(),
            (draft) => {
              const index = draft.data.findIndex(
                (x) => x.id === subscreen.announceId
              );
              if (index != -1) {
                const subscreenIndex = draft.data[
                  index
                ].announceSubScreens.findIndex((x) => x.id == subscreen.id);
                if (subscreenIndex != -1) {
                  draft.data[index].announceSubScreens.splice(
                    subscreenIndex,
                    1
                  );
                }
              }
            }
          );
          this.subject.next(updateDetailsubject);
          this.notifyService.notify('success', 'Yayın Ekranı Kaldırıldı...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(removeSubScreen$).subscribe();
  }

  getNewAnnounceCount(): Observable<number> {
    return this.announces$.pipe(
      map((announces) => {
        const newanouncecount = announces?.data.filter(
          (x) => x.isNew == true && x.isPublish == false && x.reject == false
        ).length;
        
        let photoCount=0;
        announces?.data?.forEach(x=>{
           x.announcePhotos.forEach(x=>{
              if(x.isConfirm==false && x.unConfirm==false){
                photoCount++;
              }
           })
        })

        return newanouncecount + photoCount;
      })
    );
  }

  //SignalR Events
  addNewAnnounceRealTime(model:IAnnounce):void{
    const updateSubject=produce(this.subject.getValue(),draft=>{
       draft.data.push(model);
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success', " Yeni duyuru eklendi...");
  }

  updateAnnounceRealTime(model:IAnnounce):void{
    const updateSubject=produce(this.subject.getValue(),draft=>{
      const index=draft.data.findIndex(x=>x.id===model.id);
      if(index!=-1){
        draft.data[index]=model;
      }
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success', "Duyuru Güncellendi...");
  }

  addNewPhotoRealTime(photo:IAnnouncePhoto):void{
    let announce:IAnnounce;
    const updateSubject=produce(this.subject.getValue(),draft=>{
      const index=draft.data.findIndex(x=>x.id===photo.announceId);
      if(index!=-1){
        announce=draft.data[index];
         draft.data[index].announcePhotos.push(photo);
      }
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success', " Duyuru için yeni fotoğraf eklendi...");
  }

  updatePhotoRealTime(photo:IAnnouncePhoto):void{
    let announce:IAnnounce;
    const updateSubject=produce(this.subject.getValue(),draft=>{
      const index=draft.data.findIndex(x=>x.id===photo.announceId);
      if(index!=-1){
        announce=draft.data[index];
        const photoIndex=draft.data[index].announcePhotos.findIndex(x=>x.id==photo.id);
        if(photoIndex!=-1){
          draft.data[index].announcePhotos[photoIndex]=photo;
        }
        
      }
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success', " Duyuru için fotoğraf güncellendi...");
  }

  removePhotoRealTime(photo:IAnnouncePhoto):void{
    let announce:IAnnounce;
    const updateSubject=produce(this.subject.getValue(),draft=>{
      const index=draft.data.findIndex(x=>x.id===photo.announceId);
      if(index!=-1){
        announce=draft.data[index];
        const photoIndex=draft.data[index].announcePhotos.findIndex(x=>x.id==photo.id);
        if(photoIndex!=-1){
          draft.data[index].announcePhotos.splice(photoIndex,1);
        }
      }
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success', " Duyuru için fotoğraf silindi...");
  }


  getParams(): AnnounceParams {
    return this.announceParams;
  }

  setParams(params: AnnounceParams): void {
    this.announceParams = params;
  }

  getListByParams(): void {
    this.getList(this.announceParams);
  }
}
