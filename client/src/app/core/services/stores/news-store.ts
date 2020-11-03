import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IPagination } from 'src/app/shared/models/IPagination';
import { INews } from 'src/app/shared/models/INews';
import { NewsParams } from 'src/app/shared/models/NewsParams';
import { map, catchError, tap } from 'rxjs/operators';
import produce from 'immer';
import { INewsPhoto } from 'src/app/shared/models/INewsPhoto';
import { INewsSubScreen } from 'src/app/shared/models/INewsSubScreen';


@Injectable({ providedIn: 'root' })
export class NewsStore {
  apiUrl: string = environment.apiUrl;

  newsParams = new NewsParams();

  private subject = new BehaviorSubject<IPagination<INews>>(null);
  news$: Observable<IPagination<INews>> = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService,
  ) {
    this.getList(this.newsParams);
  }

  private getList(newsParams: NewsParams) {
    let params = new HttpParams();
    if (newsParams.search) {
      params = params.append('search', newsParams.search);
    }
    if (newsParams.sort) {
      params = params.append('sort', newsParams.sort);
    }
    if (newsParams.screenId) {
      params = params.append('screenId', newsParams.screenId.toString());
    }
    if (newsParams.subScreenId) {
      params = params.append('subScreenId', newsParams.subScreenId.toString());
    }
    if (newsParams.isNew) {
      params = params.append('isNew', newsParams.isNew.toString());
    }
    if (newsParams.isPublish) {
      params = params.append('isPublish', newsParams.isPublish.toString());
    }
    if (newsParams.reject) {
      params = params.append('reject', newsParams.reject.toString());
    }

    params = params.append('pageIndex', newsParams.pageIndex.toString());
    params = params.append('pageSize', newsParams.pageSize.toString());

    const list$=this.httpClient.get<IPagination<INews>>(this.apiUrl+"news",{params})
        .pipe(
            map(news=>news),
            catchError(error=>{
                this.notifyService.notify("error",error);
                return throwError(error);
            }),
            tap(news=>{
                this.subject.next(news);
            })
        );
        this.loadingService.showLoaderUntilCompleted(list$).subscribe();
  }

  create(model: INews) {
    const create$ = this.httpClient
      .post<INews>(this.apiUrl + 'news', model)
      .pipe(
        map((news) => news),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((news) => {
          const createSubject = produce(this.subject.getValue(), (draft) => {
            draft.data.push(news);
          });
          this.subject.next(createSubject);
          this.notifyService.notify('success', 'Yeni Haber Eklendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model: Partial<INews>) {
    const update$ = this.httpClient
      .put<INews>(this.apiUrl + 'news',model)
      .pipe(
        map((news) => news),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((news) => {
          const updateSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id == news.id);
            if (index != -1) {
              draft.data[index] = news;
            }
          });
          this.subject.next(updateSubject);
          this.notifyService.notify('success', 'Haber Güncellendi...');
        })
      );
      this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  delete(model: INews) {
    const delete$ = this.httpClient
      .delete<INews>(this.apiUrl + 'news/' + model.id)
      .pipe(
        map((news) => news),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((news) => {
          const deleteSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id == news.id);
            if (index != -1) {
              draft.data.slice(index, 1);
            }
          });
          this.subject.next(deleteSubject);
          this.notifyService.notify('success', 'Haber silindi...');
        })
      );
  }

  getDetailbyId(newsId: number):Observable<INews> {
    return this.news$.pipe(
      map(news=>news?.data?.find(x=>x.id==newsId))
    );
  }

  publish(model: Partial<INews>) {
    const publish$ = this.httpClient
      .put<INews>(this.apiUrl + 'news/publish', model)
      .pipe(
        map((news) => news),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((news) => {
          const updateSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id === news.id);
            const update: INews = {
              ...draft.data[index],
              ...news,
            };
            draft.data[index] = update;
          });
          this.subject.next(updateSubject);
          this.notifyService.notify('success', 'İşlem Başarılı...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(publish$).subscribe();
  }

  addPhoto(photo: INewsPhoto) {
    const updatePhoto = produce(this.subject.getValue(), (draft) => {
      const index=draft.data.findIndex(x=>x.id==photo.newsId);
      if(index!=-1){
        draft.data[index].newsPhotos.push(photo);
      }
    });
    this.subject.next(updatePhoto);
    this.notifyService.notify('success', 'Dosya Eklendi...');
  }

  updatePhoto(photo: INewsPhoto) {
    const update$ = this.httpClient
      .put<INewsPhoto>(this.apiUrl + 'newsphoto', photo)
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
              const index=draft.data.findIndex(x=>x.id===result.newsId);
              if(index!=-1){
                const photoIndex=draft.data[index].newsPhotos.findIndex(x=>x.id==result.id);
                if(photoIndex!=-1){
                  draft.data[index].newsPhotos[photoIndex]=result;
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

  deletePhoto(model: INewsPhoto) {
    const deletePhoto$ = this.httpClient
      .delete<INewsPhoto>(this.apiUrl + 'newsphoto/' + model.id)
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
              const index=draft.data.findIndex(x=>x.id===photo.newsId);
              if(index!=-1){
                const photoIndex=draft.data[index].newsPhotos.findIndex(x=>x.id==photo.id);
                if(photoIndex!=-1){
                  draft.data[index].newsPhotos.splice(photoIndex,1);
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

  addSubScreen(model: Partial<INewsSubScreen>) {
    const addSubScreen$ = this.httpClient
      .post<INewsSubScreen>(
        this.apiUrl + 'NewsSubScreen',
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
            this.subject.getValue(),
            (draft) => {
              const index=draft.data.findIndex(x=>x.id==subscreen.newsId);
              if(index!=-1){
                draft.data[index]?.newsSubScreens.push(subscreen);
              }
            }
          );
          this.subject.next(updateDetailsubject);
          this.notifyService.notify('success', 'Yayın Ekranı Eklendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(addSubScreen$).subscribe();
  }

  removeSubScreen(model:INewsSubScreen){
    const removeSubScreen$=this.httpClient.delete<INewsSubScreen>(this.apiUrl+"NewsSubScreen/"+model.id)
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
            const index=draft.data.findIndex(x=>x.id==subscreen.newsId);
              if(index!=-1){
                const subscreenIndex=draft.data[index].newsSubScreens.findIndex(x=>x.id===subscreen.id);
                if(subscreenIndex!=-1){
                  draft.data[index].newsSubScreens.splice(subscreenIndex,1);
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
    return this.news$.pipe(
      map((announces) => {
        const newanouncecount = announces?.data.filter(
          (x) => x.isNew == true && x.isPublish == false && x.reject == false
        ).length;
        
        let photoCount=0;
        announces?.data?.forEach(x=>{
           x.newsPhotos.forEach(x=>{
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
  createNewNews(model:INews):void{
    const updateSubject=produce(this.subject.getValue(),draft=>{
          draft.data.push(model);
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success', "Yeni haber eklendi..");
  }

  updateNews(model:INews):void{
    const updateSubject=produce(this.subject.getValue(),draft=>{
      const index=draft.data.findIndex(x=>x.id===model.id);
      if(index!=-1){
        draft.data[index]=model;
      }
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success', "Yeni haber güncellendi..");
  }

  addNewPhotoRealTime(photo:INewsPhoto):void{
    const updateSubject=produce(this.subject.getValue(),draft=>{
      const index=draft.data.findIndex(x=>x.id===photo.newsId);
      if(index!=-1){
         draft.data[index].newsPhotos.push(photo);
      }
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success',"Haber için yeni fotoğraf eklendi...");
  }

  updatePhotoRealTime(photo:INewsPhoto):void{
    const updateSubject=produce(this.subject.getValue(),draft=>{
      const index=draft.data.findIndex(x=>x.id===photo.newsId);
      if(index!=-1){
        const photoIndex=draft.data[index].newsPhotos.findIndex(x=>x.id==photo.id);
        if(photoIndex!=-1){
          draft.data[index].newsPhotos[photoIndex]=photo;
        }
        
      }
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success', "Haber için fotoğraf güncellendi...");
  }

  removePhotoRealTime(photo:INewsPhoto):void{
    const updateSubject=produce(this.subject.getValue(),draft=>{
      const index=draft.data.findIndex(x=>x.id===photo.newsId);
      if(index!=-1){
        const photoIndex=draft.data[index].newsPhotos.findIndex(x=>x.id==photo.id);
        if(photoIndex!=-1){
          draft.data[index].newsPhotos.splice(photoIndex,1);
        }
      }
    });
    this.subject.next(updateSubject);
    this.notifyService.notify('success', "Haber için fotoğraf kaldırıldı...");
  }

  

  getParams(): NewsParams {
    return this.newsParams;
  }

  setParams(params: NewsParams): void {
    this.newsParams = params;
  }

  getListByParams(): void {
    this.getList(this.newsParams);
  }



}
