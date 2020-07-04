import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IVehicleCategory } from 'src/app/shared/models/IVehicleCategory';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { map, catchError, tap } from 'rxjs/operators';
import produce from 'immer';

@Injectable({providedIn: 'root'})
export class VehicleCategoryStore {
    apiUrl:string=environment.apiUrl;
    private subject=new BehaviorSubject<IVehicleCategory[]>([]);
    vehicleCategories$:Observable<IVehicleCategory[]>=this.subject.asObservable();

    constructor(
        private httpClient: HttpClient,
        private loadingService:LoadingService,
        private notifyService:NotifyService
        
    ) { 
        this.getList();
    }


    private getList() {
        const vehicleCategories$ = this.httpClient
          .get<IVehicleCategory[]>(this.apiUrl + 'vehiclecategories')
          .pipe(
            map((vehiclecategories) => vehiclecategories),
            catchError((error) => {
              this.notifyService.notify('error', error);
              return throwError(error);
            }),
            tap((vehiclecategories) => {
              this.subject.next(vehiclecategories);
            })
          );
        this.loadingService.showLoaderUntilCompleted(vehicleCategories$).subscribe();
      }
    
      create(model: IVehicleCategory) {
        const create$ = this.httpClient
          .post<IVehicleCategory>(this.apiUrl + 'vehiclecategories', model)
          .pipe(
            map((vehiclecategories) => vehiclecategories),
            catchError((error) => {
              this.notifyService.notify('error', error);
              return throwError(error);
            }),
            tap((vehiclecategories) => {
              const newItem = produce(this.subject.getValue(), (draft) => {
                draft.push(vehiclecategories);
              });
              this.subject.next(newItem);
              this.notifyService.notify('success', 'Yeni kategori eklendi...');
            })
          );
    
        this.loadingService.showLoaderUntilCompleted(create$).subscribe();
      }
    
      update(model: IVehicleCategory) {
        const update$ = this.httpClient
          .put<IVehicleCategory>(this.apiUrl + 'vehiclecategories', model)
          .pipe(
            map((vehiclecategories) => vehiclecategories),
            catchError((error) => {
              this.notifyService.notify('error', error);
              return throwError(error);
            }),
            tap((vehiclecategories) => {
              const updatedItem = produce(this.subject.getValue(), (draft) => {
                const index = draft.findIndex((x) => x.id === vehiclecategories.id);
    
                const updateItem: IVehicleCategory = {
                  ...draft[index],
                  ...vehiclecategories,
                };
    
                draft[index] = updateItem;
              });
    
              this.subject.next(updatedItem);
              this.notifyService.notify('success', 'Kategori Güncellendi...');
            })
          );
    
        this.loadingService.showLoaderUntilCompleted(update$).subscribe();
      }
    
      delete(model: IVehicleCategory) {
        const deleted$ = this.httpClient
          .delete<IVehicleCategory>(this.apiUrl + 'vehiclecategories/' + model.id)
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
              this.notifyService.notify('success', 'Kategori Silindi...');
            })
          );
    
        this.loadingService.showLoaderUntilCompleted(deleted$).subscribe();
      }


  
    
}