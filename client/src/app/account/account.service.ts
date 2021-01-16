import { Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { IUser } from './../shared/models/user';
import { ReplaySubject, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  // BehaviorSubject will eagerly emits an initial value of null even when currentUserSource has value
  // The auth guard will wait for ReplaySubject to hold a value of user before it continues to check
  private applicationUserSource = new ReplaySubject<IUser>(1); // Holds 1 user object
  applicationUser$ = this.applicationUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  loadApplicationUser(token: string) {
    if (token === null) {
      this.applicationUserSource.next(null);
      return of(null); // loadApplicationUser() in app component wants an observable to subscribe to
    }
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(this.baseUrl + 'account', { headers }).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.applicationUserSource.next(user);
        }
      })
    );
  }

  login(values: any) {
    return this.http.post(this.baseUrl + 'account/login', values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.applicationUserSource.next(user);
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.applicationUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  register(values: any) {
    return this.http.post(this.baseUrl + 'account/register', values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.applicationUserSource.next(user);
        }
      })
    );
  }

  checkEmailExists(email: string) {
    return this.http.get(this.baseUrl + 'account/emailexists?email=' + email);
  }
}
