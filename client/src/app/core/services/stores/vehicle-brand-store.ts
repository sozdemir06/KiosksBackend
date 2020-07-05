import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IPagination } from 'src/app/shared/models/IPagination';
import { IVehicleBrand } from 'src/app/shared/models/IVehicleBrand';
import { VehicleBrandParams } from 'src/app/shared/models/VehicleBrandParams';
import { map, catchError, tap } from 'rxjs/operators';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import produce from 'immer';

@Injectable({ providedIn: 'root' })
export class VehicleBrandStore {
  apiUrl: string = environment.apiUrl;
  private subject = new BehaviorSubject<IPagination<IVehicleBrand>>(null);
  vehicleBrands$: Observable<
    IPagination<IVehicleBrand>
  > = this.subject.asObservable();

  vehicleBrandParams = new VehicleBrandParams();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {
    this.getList(this.vehicleBrandParams);
  }

  private getList(vehicleBrandParams: VehicleBrandParams) {
    let params = new HttpParams();

    if (vehicleBrandParams.search) {
      params = params.append('search', vehicleBrandParams.search);
    }
    if (vehicleBrandParams.sort) {
      params = params.append('sort', vehicleBrandParams.sort);
    }
    if (vehicleBrandParams.vehicleCategoryId) {
      params = params.append(
        'vehicleCategoryId',
        vehicleBrandParams.vehicleCategoryId.toString()
      );
    }

    params = params.append(
      'pageIndex',
      vehicleBrandParams.pageIndex.toString()
    );
    params = params.append('pageSize', vehicleBrandParams.pageSize.toString());

    const vehicleBrandList$ = this.httpClient
      .get<IPagination<IVehicleBrand>>(this.apiUrl + 'vehiclebrands', {
        params,
      })
      .pipe(
        map((vehicleBrands) => vehicleBrands),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((vehicleBrands) => {
          this.subject.next(vehicleBrands);
        })
      );
    this.loadingService.showLoaderUntilCompleted(vehicleBrandList$).subscribe();
  }

  create(model: IVehicleBrand) {
    const create$ = this.httpClient
      .post<IVehicleBrand>(this.apiUrl + 'vehiclebrands', model)
      .pipe(
        map((brands) => brands),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((brands) => {
          const createdNew = produce(this.subject.getValue(), (draft) => {
            draft.data.push(brands);
          });
          this.subject.next(createdNew);
          this.notifyService.notify('success', 'Araç markası eklendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model: Partial<IVehicleBrand>) {
    const update$ = this.httpClient
      .put<IVehicleBrand>(this.apiUrl + 'vehiclebrands', model)
      .pipe(
        map((brands) => brands),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((brands) => {
          const updatedSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id === brands.id);
            const updated: IVehicleBrand = {
              ...draft.data[index],
              ...brands,
            };
            draft.data[index] = updated;
          });
          this.subject.next(updatedSubject);
          this.notifyService.notify('success', 'Araç markası güncellendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  delete(model: IVehicleBrand) {
    const delete$ = this.httpClient
      .delete<IVehicleBrand>(this.apiUrl + 'vehiclebrands/' + model.id)
      .pipe(
        map((brands) => brands),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((brands) => {
          const deletedSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id === brands.id);
            if (index != -1) {
              draft.data.splice(index, 1);
            }
          });
          this.subject.next(deletedSubject);
          this.notifyService.notify('success', 'Araç markası silindi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(delete$).subscribe();
  }

  setVehicleBrandParams(vehicleBrandParams: VehicleBrandParams) {
    this.vehicleBrandParams = vehicleBrandParams;
  }

  getVehicleBrandParams() {
    return this.vehicleBrandParams;
  }

  getVehicleBrandList() {
    this.getList(this.vehicleBrandParams);
  }
}
