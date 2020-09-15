import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IDegree } from 'src/app/shared/models/IDegree';
import { catchError, tap, map } from 'rxjs/operators';
import { LoadingService } from '../loading-service';
import { NotifyService } from '../notify-service';
import { DegreeParams } from 'src/app/shared/models/DegreeParams';
import { IPagination } from 'src/app/shared/models/IPagination';
import produce from 'immer';

@Injectable({ providedIn: 'root' })
export class DegreeStore {
  apiUrl: string = environment.apiUrl;
  private subject = new BehaviorSubject<IPagination<IDegree>>(null);
  degrees$: Observable<IPagination<IDegree>> = this.subject.asObservable();

  degreeParams = new DegreeParams();

  constructor(
    private httpClient: HttpClient,
    private loadingService: LoadingService,
    private notifyService: NotifyService
  ) {
    this.getList(this.degreeParams);
  }

  private getList(degreeParams: DegreeParams) {
    let params = new HttpParams();

    if (degreeParams.search) {
      params = params.append('search', degreeParams.search);
    }
    if (degreeParams.sort) {
      params = params.append('sort', degreeParams.sort);
    }

    params = params.append('pageIndex', degreeParams.pageIndex.toString());
    params = params.append('pageSize', degreeParams.pageSize.toString());

    const vehicleBrandList$ = this.httpClient
      .get<IPagination<IDegree>>(this.apiUrl + 'degrees', {
        params,
      })
      .pipe(
        map((degrees) => degrees),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((degrees) => {
          this.subject.next(degrees);
        })
      );
    this.loadingService.showLoaderUntilCompleted(vehicleBrandList$).subscribe();
  }

  create(model: IDegree) {
    const create$ = this.httpClient
      .post<IDegree>(this.apiUrl + 'degrees', model)
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
          this.notifyService.notify('success', 'Yeni Ünvan eklendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(create$).subscribe();
  }

  update(model: Partial<IDegree>) {
    const update$ = this.httpClient
      .put<IDegree>(this.apiUrl + 'degrees', model)
      .pipe(
        map((brands) => brands),
        catchError((error) => {
          this.notifyService.notify('error', error);
          return throwError(error);
        }),
        tap((brands) => {
          const updatedSubject = produce(this.subject.getValue(), (draft) => {
            const index = draft.data.findIndex((x) => x.id === brands.id);
            const updated: IDegree = {
              ...draft.data[index],
              ...brands,
            };
            draft.data[index] = updated;
          });
          this.subject.next(updatedSubject);
          this.notifyService.notify('success', 'Ünvan güncellendi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(update$).subscribe();
  }

  delete(model: IDegree) {
    const delete$ = this.httpClient
      .delete<IDegree>(this.apiUrl + 'degrees/' + model.id)
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
          this.notifyService.notify('success', 'Ünvan silindi...');
        })
      );
    this.loadingService.showLoaderUntilCompleted(delete$).subscribe();
  }

  getDegreeList(): Observable<IDegree[]> {
    return this.httpClient.get<IDegree[]>(this.apiUrl + 'degrees/list');
  }

  setParams(degreeParams: DegreeParams) {
    this.degreeParams = degreeParams;
  }

  getParams() {
    return this.degreeParams;
  }

  getListWithParams() {
    this.getList(this.degreeParams);
  }
}
