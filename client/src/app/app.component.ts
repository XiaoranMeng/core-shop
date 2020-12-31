import { CartService } from './cart/cart.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Core Shop';
  products: any[];

  constructor(private cartService: CartService) { }

  ngOnInit(): void {
    const cartId = localStorage.getItem('cart_id');
    if (cartId) {
      this.cartService.getCart(cartId).subscribe(() => {
        console.log('Cart retrived');
      }, error => {
        console.log(error);
      });
    }
  }
}
