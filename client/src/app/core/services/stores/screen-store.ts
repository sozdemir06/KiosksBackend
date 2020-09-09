import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, throwError, Observable } from 'rxjs';
import { IScreen } from 'src/app/shared/models/IScreen';
import { map, catchError, tap } from 'rxjs/operators';
import produce from 'immer';
import { IScreenHeader } from 'src/app/shared/models/IScreenHeader';
import { IScreenFooter } from 'src/app/shared/models/IScreenFooter';
import { IScreenHeaderPhoto } from 'src/app/shared/models/IScreenHeaderPhoto';

@Injectable({ providedIn: 'root' })
export class ScreenStore {
  apiUrl: string = environment.apiUrl;

  private subject = new BehaviorSubject<IScreen[]>([]);
  screens$: Observable<IScreen[]> = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {
    this.getList();
  }

  private getList() {
    const getList$ = this.httpClient
      .get<IScreen[]>(this.apiUrl + 'screens')
      .pipe(
        map((screens) => screens),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((screens) => {
          this.subject.next(screens);
        })
      );
    this.loadingService.showLoaderUntilCompleted(getList$).subscribe();
  }

  create(model: IScreen) {
    const create$ = this.httpClient
      .post<IScreen>(this.apiUrl + 'screens', model)
      .pipe(
        map((screens) => screens),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((screens) => {
          const createdNew = produce(this.subject.getValue(), (draft) => {
            draft.push(screens);
          });
          this.subject.next(createdNew);
          this.notifyService.notify('success', 'Ekran Eklendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model:Partial<IScreen>){
    const update$=this.httpClient.put<IScreen>(this.apiUrl+"screens",model)
    .pipe(
      map((screens) => screens),
      catchError((error) => {
        this.notifyService.notify('error', error);
        return throwError(error);
      }),
      tap((screens) => {
        const createdNew = produce(this.subject.getValue(), (draft) => {
           const index=draft.findIndex(x=>x.id==screens.id);
           const updatedItem:IScreen={
              ...draft[index],
              ...screens
           };
           draft[index]=updatedItem;
        });
        this.subject.next(createdNew);
        this.notifyService.notify('success', 'Ekran Güncellendi...');
      })
    );
    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  delete(model:IScreen){
    const delete$=this.httpClient.delete<IScreen>(this.apiUrl+"screens/"+model.id)
        .pipe(
          map(screen=>screen),
          catchError(error=>{
            this.notifyService.notify("error",error);
            return throwError(error);
          }),
          tap(screen=>{
            const deleteFromSubject=produce(this.subject.getValue(),draft=>{
              const index=draft.findIndex(x=>x.id==screen.id);
              if(index!=-1)
              {
                draft.splice(index,1);
              }
            });
            this.subject.next(deleteFromSubject);
            this.notifyService.notify('success', 'Ekran Silindi...');
          })
        );
        this.loadingService.showLoaderUntilCompleted(delete$).subscribe();
  }

  //Create Screen HEader
  createScreenHeader(screenHeader:IScreenHeader){
    const create$=this.httpClient.post<IScreenHeader>(this.apiUrl+"screenheaders",screenHeader)
      .pipe(
        map(screenheaders=>screenheaders),
        catchError(error=>{
          this.notifyService.notify("error",error);
          return throwError(error);
        }),
        tap(screenheaders=>{
          const updateSubject=produce(this.subject.getValue(),draft=>{
            const index=draft.findIndex(x=>x.id===screenheaders.screenId);
            if(index!=-1){
              draft[index].screenHeaders=screenheaders;
            }
          });
          this.subject.next(updateSubject);
          this.notifyService.notify("success","Üst Başlık Bilgisi Eklendi...");
        })
      );
      this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }
  //Update Screen HEader
  updateScreenHeader(screenHeader:IScreenHeader){
    const update$=this.httpClient.put<IScreenHeader>(this.apiUrl+"screenheaders",screenHeader)
      .pipe(
        map(screenheaders=>screenheaders),
        catchError(error=>{
          this.notifyService.notify("error",error);
          return throwError(error);
        }),
        tap(screenheaders=>{
          const updateSubject=produce(this.subject.getValue(),draft=>{
            const index=draft.findIndex(x=>x.id===screenheaders.screenId);
            if(index!=-1){
              draft[index].screenHeaders=screenheaders;
            }
          });
          this.subject.next(updateSubject);
          this.notifyService.notify("success","Üst Başlık Bilgisi Güncellendi...");
        })
      );
      this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

   //Create Screen Footer
   createScreenFooter(screenHeader:IScreenFooter){
    const create$=this.httpClient.post<IScreenFooter>(this.apiUrl+"screenfooters",screenHeader)
      .pipe(
        map(screenfooters=>screenfooters),
        catchError(error=>{
          this.notifyService.notify("error",error);
          return throwError(error);
        }),
        tap(screenfooters=>{
          const updateSubject=produce(this.subject.getValue(),draft=>{
            const index=draft.findIndex(x=>x.id===screenfooters.screenId);
            if(index!=-1){
              draft[index].screenFooters=screenfooters;
            }
          });
          this.subject.next(updateSubject);
          this.notifyService.notify("success","Alt Bilgisi Eklendi...");
        })
      );
      this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }
   //Update Screen Footer
   updateScreenFooter(screenHeader:IScreenFooter){
    const create$=this.httpClient.put<IScreenFooter>(this.apiUrl+"screenfooters",screenHeader)
      .pipe(
        map(screenfooters=>screenfooters),
        catchError(error=>{
          this.notifyService.notify("error",error);
          return throwError(error);
        }),
        tap(screenfooters=>{
          const updateSubject=produce(this.subject.getValue(),draft=>{
            const index=draft.findIndex(x=>x.id===screenfooters.screenId);
            if(index!=-1){
              draft[index].screenFooters=screenfooters;
            }
          });
          this.subject.next(updateSubject);
          this.notifyService.notify("success","Alt Başlık Bilgisi Güncellendi...");
        })
      );
      this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }

  crateScreenHeaderPhoto(model:IScreenHeaderPhoto,screenId?:number){
    const addScreenHaderPhoto=produce(this.subject.getValue(),draft=>{
      const index=draft.findIndex(x=>x.id===model.screenId);
      if(index!=-1){
        draft[index].screenHeaderPhotos.push(model);
      }
    });
    this.subject.next(addScreenHaderPhoto);
    this.notifyService.notify("success","Logo eklendi...");
  }

  updateScreenHeaderPhoto(model:IScreenHeaderPhoto){
    const update$=this.httpClient.put<IScreenHeaderPhoto>(this.apiUrl+"screenheaderphotos",model)
      .pipe(
        map(photo=>photo),
        catchError(error=>{
          this.notifyService.notify("error",error);
          return throwError(error);
        }),
        tap(photo=>{
          const updatedSubject=produce(this.subject.getValue(),draft=>{
            const index=draft.findIndex(x=>x.id==photo.screenId);
            const photoIndex=draft[index].screenHeaderPhotos.findIndex(x=>x.id===photo.id);

            if(index!=-1 && photoIndex!=-1){
              const update={
                ...draft[index].screenHeaderPhotos[photoIndex],
                ...photo
              };

              draft[index].screenHeaderPhotos[photoIndex]=update;
            }
          });
          this.subject.next(updatedSubject);
          this.notifyService.notify("success","İşlem Başarılı...");
        })
      );
      this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }
  setIsMainScreenHeaderPhoto(model:IScreenHeaderPhoto){
    const update$=this.httpClient.put<IScreenHeaderPhoto>(this.apiUrl+"screenheaderphotos/setmain",model)
      .pipe(
        map(photo=>photo),
        catchError(error=>{
          this.notifyService.notify("error",error);
          return throwError(error);
        }),
        tap(photo=>{
          const updatedSubject=produce(this.subject.getValue(),draft=>{
            const index=draft.findIndex(x=>x.id==photo.screenId);
            const photoIndex=draft[index].screenHeaderPhotos.findIndex(x=>x.id===photo.id);
            const isMain=draft[index].screenHeaderPhotos.find(x=>x.isMain==true);
            
            if(isMain){
              isMain.isMain=false;
            }
            
            if(index!=-1 && photoIndex!=-1){
              const update={
                ...draft[index].screenHeaderPhotos[photoIndex],
                ...photo
              };
              
              draft[index].screenHeaderPhotos[photoIndex]=update;
            }
          });
          this.subject.next(updatedSubject);
          this.notifyService.notify("success","İşlem Başarılı...");
        })
      );
      this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  deleteScreenHeaderPhoto(model:IScreenHeaderPhoto){
    const delete$=this.httpClient.delete<IScreenHeaderPhoto>(this.apiUrl+"screenheaderphotos/"+model.id)
          .pipe(
            map(photo=>photo),
            catchError(error=>{
              this.notifyService.notify("error",error);
              return throwError(error);
            }),
            tap(photo=>{
              const updatedSubject=produce(this.subject.getValue(),draft=>{
                 const index=draft.findIndex(x=>x.id===photo.screenId);
                 if(index!=-1){
                    const photoIndex=draft[index].screenHeaderPhotos.findIndex(x=>x.id===photo.id);
                    draft[index].screenHeaderPhotos.splice(photoIndex,1);
                 }
              });
              this.subject.next(updatedSubject);
              this.notifyService.notify("success","Resim Başarılı...");
            })
          );
          this.loadingService.showLoaderUntilCompleted(delete$).subscribe();
  }


  getScreenById(screendId:number):Observable<IScreen>{
    return this.screens$.pipe(map(screens=>screens.find(x=>x.id===screendId)));
  }
}
