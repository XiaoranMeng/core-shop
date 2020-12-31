import { ICartTotals } from './../../models/cart';
import { Observable } from 'rxjs';
import { CartService } from './../../../cart/cart.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.scss']
})
export class OrderTotalsComponent implements OnInit {
  cartTotal$: Observable<ICartTotals>;

  constructor(private cartService: CartService) { }

  ngOnInit(): void {
    this.cartTotal$ = this.cartService.cartTotal$;
  }

}
