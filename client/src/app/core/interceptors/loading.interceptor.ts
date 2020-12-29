import { delay, finalize } from 'rxjs/operators';
import { LoaderService } from './../services/loader.service';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Observable } from "rxjs";
import { Injectable } from '@angular/core';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

    constructor(private loader: LoaderService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.loader.load();
        return next.handle(req).pipe(
            delay(1000),
            finalize(() => {
                this.loader.idle();
            })
        );
    }
}