import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { IBuildingAge } from 'src/app/shared/models/IBuildingAge';
import { NotifyService } from '../notify-service';
import { LoadingService } from '../loading-service';
import { map, catchError, tap } from 'rxjs/operators';

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
        })
      );
    this.loadingService.showLoaderUntilCompleted(buildingAgeList$).subscribe();
  }
}
