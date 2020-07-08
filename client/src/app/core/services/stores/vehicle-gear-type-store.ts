import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IVehicleGearType } from 'src/app/shared/models/IVehicleGearType';
import { map, catchError, tap } from 'rxjs/operators';
import produce from 'immer';

@Injectable({providedIn: 'root'})
export class VehicleGearTypeStore {
    apiUrl:string=environment.apiUrl;

    private subject=new BehaviorSubject<IVehicleGearType[]>([]);
    vehicleGearTypes$:Observable<IVehicleGearType[]>=this.subject.asObservable();

    constructor(
        private httpClient: HttpClient,
        private loadingService:LoadingService,
        private notifyService:NotifyService
        
        ) { 
            this.getList();
        }


        private getList() {
            const buildingAgeList$ = this.httpClient
              .get<IVehicleGearType[]>(this.apiUrl + 'vehicleGearTypes')
              .pipe(
                map((vehicleGearType) => vehicleGearType),
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
        
          create(model: IVehicleGearType) {
            const create$ = this.httpClient
              .post<IVehicleGearType>(this.apiUrl + 'vehicleGearTypes', model)
              .pipe(
                map((vehicleGearType) => vehicleGearType),
                catchError((error) => {
                  this.notifyService.notify('error', error);
                  return throwError(error);
                }),
                tap((vehicleGearType) => {
                  const newItem = produce(this.subject.getValue(), (draft) => {
                    draft.push(vehicleGearType);
                  });
                  this.subject.next(newItem);
                  this.notifyService.notify('success', 'Yeni opsiyon eklendi...');
                })
              );
        
            this.loadingService.showLoaderUntilCompleted(create$).subscribe();
          }
        
          update(model: IVehicleGearType) {
            const update$ = this.httpClient
              .put<IVehicleGearType>(this.apiUrl + 'vehicleGearTypes', model)
              .pipe(
                map((vehicleGearType) => vehicleGearType),
                catchError((error) => {
                  this.notifyService.notify('error', error);
                  return throwError(error);
                }),
                tap((vehicleGearType) => {
                  const updatedItem = produce(this.subject.getValue(), (draft) => {
                    const index = draft.findIndex((x) => x.id === vehicleGearType.id);
        
                    const updateItem: IVehicleGearType = {
                      ...draft[index],
                      ...vehicleGearType,
                    };
        
                    draft[index] = updateItem;
                  });
        
                  this.subject.next(updatedItem);
                  this.notifyService.notify('success', 'Güncelleme işlemi başarılı');
                })
              );
        
            this.loadingService.showLoaderUntilCompleted(update$).subscribe();
          }
        
          delete(model: IVehicleGearType) {
            const deleted$ = this.httpClient
              .delete<IVehicleGearType>(this.apiUrl + 'vehicleGearTypes/' + model.id)
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