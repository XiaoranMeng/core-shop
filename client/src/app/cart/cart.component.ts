import { CartService } from './cart.service';
import { ICart, ICartItem } from './../shared/models/cart';
import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  cart$: Observable<ICart>;

  constructor(private cartService: CartService) { }

  ngOnInit(): void {
    this.cart$ = this.cartService.cart$;
  }

  incrementQuantity(cartItem: ICartItem) {
    this.cartService.incrementQuantity(cartItem);
  }

  decrementQuantity(cartItem: ICartItem) {
    this.cartService.decrementQuantity(cartItem);
  }

  removeCartItem(cartItem: ICartItem) {
    this.cartService.removeCartItem(cartItem);
  }

}
