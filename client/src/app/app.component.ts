import { AccountService } from './account/account.service';
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

  constructor(
    private cartService: CartService,
    private accountService: AccountService) { }

  ngOnInit(): void {
   this.loadCart();
   this.loadApplicationUser();
  }

  loadApplicationUser() {
    const token = localStorage.getItem('token');
    // Pass null as the toke if not exists
    this.accountService.loadApplicationUser(token).subscribe(() => {
      console.log('Application user loaded');
    }, error => {
      console.log(error);
    });
  }

  loadCart() {
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
