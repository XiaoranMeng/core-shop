import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, delay } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private router: Router, private toastr: ToastrService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError(response => {
                if (response) {
                    const { status, error } = response;

                    if (status === 400) {
                        if (error.errors) {
                            throw error;
                        } else {
                            this.toastr.error(error.message, error.statusCode);
                        }
                    }

                    if (status === 401) {
                        this.toastr.error(error.message, error.statusCode);
                    }

                    if (status === 404) {
                        this.router.navigateByUrl('/not-found');
                    }

                    if (status === 500) {
                        const navigationExtras: NavigationExtras = {
                            state: {
                                error
                            }
                        };
                        this.router.navigateByUrl('/server-error', navigationExtras);
                    }
                }
                return throwError(response);
            })
        );
    }
}
