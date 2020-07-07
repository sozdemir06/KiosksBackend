import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IVehicleModel } from 'src/app/shared/models/IVehicleModel';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { VehicleModelParams } from 'src/app/shared/models/VehicleModelParams';
import { VehicleBrandParams } from 'src/app/shared/models/VehicleBrandParams';
import { map, catchError, tap } from 'rxjs/operators';
import produce from 'immer';
import { IPagination } from 'src/app/shared/models/IPagination';

@Injectable({ providedIn: 'root' })
export class VehicleModelStore {
  apiUrl: string = environment.apiUrl;

  private subject = new BehaviorSubject<IPagination<IVehicleModel>>(null);
  vehicleModels$: Observable<
    IPagination<IVehicleModel>
  > = this.subject.asObservable();

  vehicleModelParams = new VehicleModelParams();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {
    this.getList(this.vehicleModelParams);
  }

  private getList(vehicleModelParams: VehicleBrandParams) {
    let params = new HttpParams();

    if (vehicleModelParams.search) {
      params = params.append('search', vehicleModelParams.search);
    }
    if (vehicleModelParams.sort) {
      params = params.append('sort', vehicleModelParams.sort);
    }
    if (vehicleModelParams.vehicleCategoryId) {
      params = params.append(
        'vehicleCategoryId',
        vehicleModelParams.vehicleCategoryId.toString()
      );
    }
    if (vehicleModelParams.vehicleBrandId) {
      params = params.append(
        'vehicleBrandId',
        vehicleModelParams.vehicleBrandId.toString()
      );
    }

    params = params.append(
      'pageIndex',
      vehicleModelParams.pageIndex.toString()
    );
    params = params.append('pageSize', vehicleModelParams.pageSize.toString());

    const vehicleModelList$ = this.httpClient
      .get<IPagination<IVehicleModel>>(this.apiUrl + 'vehiclemodels', {
        params,
      })
      .pipe(
        map((vehicleModels) => vehicleModels),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((vehicleModels) => {
          this.subject.next(vehicleModels);
        })
      );
    this.loadingService.showLoaderUntilCompleted(vehicleModelList$).subscribe();
  }

  create(model: IVehicleModel) {
    const create$ = this.httpClient
      .post<IVehicleModel>(this.apiUrl + 'vehiclemodels', model)
      .pipe(
        map((vehicleModels) => vehicleModels),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((vehicleModels) => {
          const createdSubject = produce(this.subject.getValue(), (draft) => {
            draft.data.push(vehicleModels);
          });

          this.subject.next(createdSubject);
          this.notifyService.notify('success', 'Araç modeli eklendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model: Partial<IVehicleModel>) {
    const update$ = this.httpClient
      .put<IVehicleModel>(this.apiUrl + 'vehiclemodels', model)
      .pipe(
        map((vehicleModels) => vehicleModels),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((vehicleModels) => {
          const createdSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex(
              (x) => x.id === vehicleModels.id
            );

            const updatedItem: IVehicleModel = {
              ...draft.data[index],
              ...vehicleModels,
            };
            draft.data[index] = updatedItem;
          });

          this.subject.next(createdSubject);
          this.notifyService.notify('success', 'Araç modeli Güncellendi...');
        })
      );
      this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  delete(model: IVehicleModel) {
    const delete$ = this.httpClient
      .delete<IVehicleModel>(this.apiUrl + 'vehiclemodels/' + model.id)
      .pipe(
        map((vehicleModels) => vehicleModels),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((vehicleModels) => {
          const deletedSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex(
              (x) => x.id === vehicleModels.id
            );
            if (index != -1) {
              draft.data.splice(index, 1);
            }
          });

          this.subject.next(deletedSubject);
          this.notifyService.notify('success', 'Araç modeli Güncellendi...');
        })
      );

      this.loadingService.showLoaderUntilCompleted(delete$).subscribe();
  }

  getVehicleModelParams(){
    return this.vehicleModelParams;
  }

  setVehicleModelParams(vehicleModelParams:VehicleModelParams){
    this.vehicleModelParams=vehicleModelParams;
  }

  getVehicleModelList(){
    this.getList(this.vehicleModelParams);
  }
}
