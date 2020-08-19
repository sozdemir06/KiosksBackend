import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  RouterStateSnapshot,
  Router,
} from '@angular/router';
import { AuthStore } from 'src/app/auth/auth.store';
import { NotifyService } from '../services/notify-service';
import { tap, map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(
    private authStore: AuthStore,
    private notifyService: NotifyService,
    private router: Router
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const routeRoles = route.firstChild?.data['roles'] as Array<string>;

    if (routeRoles) {
      let isMatch = this.authStore.isMatchRoles(routeRoles);
      if (isMatch) {
        return true;
      } else {
        this.notifyService.notify(
          'error',
          'Bu Link için yetkiniz bulunmamaktadır...'
        );
        this.router.navigateByUrl('/');

        return false;
      }
    } else if (!routeRoles) {
      return true;
    }

    this.authStore.isLoggedIn$
      .pipe(
        map((isLoggetIn) => isLoggetIn),
        tap((loggeIn) => {
          if (loggeIn) {
            return true;
          } else if (!loggeIn) {
            this.notifyService.notify('error', 'Giriş Yapmalısınız...');
            this.router.navigateByUrl('/auth');
            return false;
          }
        })
      )
      .subscribe();
  }
}
