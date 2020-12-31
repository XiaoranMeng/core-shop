import { CartRoutingModule } from './cart-routing.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartComponent } from './cart.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [CartComponent],
  imports: [
    SharedModule,
    CommonModule,
    CartRoutingModule
  ]
})
export class CartModule { }
