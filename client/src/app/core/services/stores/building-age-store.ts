import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IBuildingAge } from 'src/app/shared/models/IBuildingAge';
import { NotifyService } from '../notify-service';
import { LoadingService } from '../loading-service';
import { map, catchError, tap, shareReplay } from 'rxjs/operators';
import produce from 'immer';

@Injectable({ providedIn: 'root' })
export class BuildingAgeStore {
  apiUrl: string = environment.apiUrl;

  private subject = new BehaviorSubject<IBuildingAge[]>([]);
  buildingsAge$: Observable<IBuildingAge[]> = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private notifyService: NotifyService,
    private loadingService: LoadingService
  ) {
    this.getList();
  }

  private getList() {
    const buildingAgeList$ = this.httpClient
      .get<IBuildingAge[]>(this.apiUrl + 'buildingsage')
      .pipe(
        map((buildingsage) => buildingsage),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((buildingage) => {
          this.subject.next(buildingage);
        }),
        shareReplay()
      );
    this.loadingService.showLoaderUntilCompleted(buildingAgeList$).subscribe();
  }

  create(model: IBuildingAge) {
    const create$ = this.httpClient
      .post<IBuildingAge>(this.apiUrl + 'buildingsage', model)
      .pipe(
        map((buildingsage) => buildingsage),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((buildingsage) => {
          const newItem = produce(this.subject.getValue(), (draft) => {
            draft.push(buildingsage);
          });
          this.subject.next(newItem);
          this.notifyService.notify('success', 'Yeni opsiyon eklendi...');
        })
      );

    this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model: IBuildingAge) {
    const update$ = this.httpClient
      .put<IBuildingAge>(this.apiUrl + 'buildingsage', model)
      .pipe(
        map((buildingsage) => buildingsage),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((buildingsage) => {
          const updatedItem = produce(this.subject.getValue(), (draft) => {
            const index = draft.findIndex((x) => x.id === buildingsage.id);

            const updateItem: IBuildingAge = {
              ...draft[index],
              ...buildingsage
            };

            draft[index] = updateItem;
          });

          this.subject.next(updatedItem);
          this.notifyService.notify('success', 'Güncelleme işlemi başarılı');
        })
      );

    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  delete(model:IBuildingAge){
    const deleted$ = this.httpClient
      .delete<IBuildingAge>(this.apiUrl + 'buildingsage/' + model.id)
      .pipe(
        map((data) => data),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap(data=>{
            const deletedItem=produce(this.subject.getValue(),draft=>{
                const index=draft.findIndex(x=>x.id===data.id);
                if(index!==-1){
                    draft.splice(index,1);
                }
            });
            this.subject.next(deletedItem);
            this.notifyService.notify("success","Silindi...");
        })
      );

      this.loadingService.showLoaderUntilCompleted(deleted$).subscribe();
  }
}
