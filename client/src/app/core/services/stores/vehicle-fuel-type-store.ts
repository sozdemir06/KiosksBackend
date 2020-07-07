import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IVehicleFuelType } from 'src/app/shared/models/IVehicleFuelType';
import { map, catchError, tap } from 'rxjs/operators';
import produce from 'immer';

@Injectable({ providedIn: 'root' })
export class VehicleFuelTypeStore {
  apiUrl: string = environment.apiUrl;
  private subject = new BehaviorSubject<IVehicleFuelType[]>([]);
  vehicleFuelTypes$: Observable<
    IVehicleFuelType[]
  > = this.subject.asObservable();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {
    this.getList();
  }

  private getList() {
    const buildingAgeList$ = this.httpClient
      .get<IVehicleFuelType[]>(this.apiUrl + 'vehiclefueltypes')
      .pipe(
        map((vehicleFuelType) => vehicleFuelType),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((buildingage) => {
          this.subject.next(buildingage);
        })
      );
    this.loadingService.showLoaderUntilCompleted(buildingAgeList$).subscribe();
  }

  create(model: IVehicleFuelType) {
    const create$ = this.httpClient
      .post<IVehicleFuelType>(this.apiUrl + 'vehiclefueltypes', model)
      .pipe(
        map((vehicleFuelType) => vehicleFuelType),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((vehicleFuelType) => {
          const newItem = produce(this.subject.getValue(), (draft) => {
            draft.push(vehicleFuelType);
          });
          this.subject.next(newItem);
          this.notifyService.notify('success', 'Yeni opsiyon eklendi...');
        })
      );

    this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model: IVehicleFuelType) {
    const update$ = this.httpClient
      .put<IVehicleFuelType>(this.apiUrl + 'vehiclefueltypes', model)
      .pipe(
        map((vehicleFuelType) => vehicleFuelType),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((vehicleFuelType) => {
          const updatedItem = produce(this.subject.getValue(), (draft) => {
            const index = draft.findIndex((x) => x.id === vehicleFuelType.id);

            const updateItem: IVehicleFuelType = {
              ...draft[index],
              ...vehicleFuelType,
            };

            draft[index] = updateItem;
          });

          this.subject.next(updatedItem);
          this.notifyService.notify('success', 'Güncelleme işlemi başarılı');
        })
      );

    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  delete(model: IVehicleFuelType) {
    const deleted$ = this.httpClient
      .delete<IVehicleFuelType>(this.apiUrl + 'vehiclefueltypes/' + model.id)
      .pipe(
        map((data) => data),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((data) => {
          const deletedItem = produce(this.subject.getValue(), (draft) => {
            const index = draft.findIndex((x) => x.id === data.id);
            if (index !== -1) {
              draft.splice(index, 1);
            }
          });
          this.subject.next(deletedItem);
          this.notifyService.notify('success', 'Silindi...');
        })
      );

    this.loadingService.showLoaderUntilCompleted(deleted$).subscribe();
  }
}
