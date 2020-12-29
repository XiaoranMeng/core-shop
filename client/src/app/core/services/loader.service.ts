import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {
  counter = 0;

  constructor(private spinnerService: NgxSpinnerService) { }

  load() {
    this.counter++;
    this.spinnerService.show(undefined, {
      size: 'large',
      fullScreen: true,
      type: 'cube-transition',
      bdColor: 'rgba(0, 0, 0, 0.8)',
      color: '#16CFAC'
    });
  }

  idle() {
    this.counter--;
    if (this.counter <= 0) {
      this.counter = 0;
      this.spinnerService.hide();
    }
  }
}
