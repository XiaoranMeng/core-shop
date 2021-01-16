import { map } from 'rxjs/operators';
import { AccountService } from './../../account/account.service';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private accountService: AccountService,
    private router: Router) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
      return this.accountService.applicationUser$.pipe(
        map(user => {
          if (user) {
            return true;
          }
          this.router.navigate(['account/login'], {
            queryParams: {
              returnUrl: state.url // Return to checkout
            }
          });
        })
      );
  }
}