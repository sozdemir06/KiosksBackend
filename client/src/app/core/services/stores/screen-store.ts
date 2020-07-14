import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, throwError, Observable } from 'rxjs';
import { IScreen } from 'src/app/shared/models/IScreen';
import { map, catchError, tap } from 'rxjs/operators';
import produce from 'immer';
import { unsupported } from '@angular/compiler/src/render3/view/util';

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

  getScreenById(screendId:number):Observable<IScreen>{
    return this.screens$.pipe(map(screens=>screens.find(x=>x.id===screendId)));
  }
}
