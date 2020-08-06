import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IVehicleEngineSize } from 'src/app/shared/models/IVehicleEngineSize';
import { map, catchError, tap, shareReplay } from 'rxjs/operators';
import produce from 'immer';

@Injectable({ providedIn: 'root' })
export class VehicleEngineSizeStore {
  apiUrl: string = environment.apiUrl;
  private subject = new BehaviorSubject<IVehicleEngineSize[]>([]);
  vehicleEngineSizes$: Observable<
    IVehicleEngineSize[]
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
      .get<IVehicleEngineSize[]>(this.apiUrl + 'vehicleenginesizes')
      .pipe(
        map((vehicleEngineSizes) => vehicleEngineSizes),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((vehicleEngineSizes) => {
          this.subject.next(vehicleEngineSizes);
        }),
        shareReplay()
      );
    this.loadingService.showLoaderUntilCompleted(buildingAgeList$).subscribe();
  }

  create(model: IVehicleEngineSize) {
    const create$ = this.httpClient
      .post<IVehicleEngineSize>(this.apiUrl + 'vehicleenginesizes', model)
      .pipe(
        map((vehicleEngineSizes) => vehicleEngineSizes),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((vehicleEngineSizes) => {
          const newItem = produce(this.subject.getValue(), (draft) => {
            draft.push(vehicleEngineSizes);
          });
          this.subject.next(newItem);
          this.notifyService.notify('success', 'Yeni opsiyon eklendi...');
        })
      );

    this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model: IVehicleEngineSize) {
    const update$ = this.httpClient
      .put<IVehicleEngineSize>(this.apiUrl + 'vehicleenginesizes', model)
      .pipe(
        map((vehicleEngineSizes) => vehicleEngineSizes),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((vehicleEngineSizes) => {
          const updatedItem = produce(this.subject.getValue(), (draft) => {
            const index = draft.findIndex(
              (x) => x.id === vehicleEngineSizes.id
            );

            const updateItem: IVehicleEngineSize = {
              ...draft[index],
              ...vehicleEngineSizes,
            };

            draft[index] = updateItem;
          });

          this.subject.next(updatedItem);
          this.notifyService.notify('success', 'Güncelleme işlemi başarılı');
        })
      );

    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  delete(model: IVehicleEngineSize) {
    const deleted$ = this.httpClient
      .delete<IVehicleEngineSize>(
        this.apiUrl + 'vehicleenginesizes/' + model.id
      )
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
