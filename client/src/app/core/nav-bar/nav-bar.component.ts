import { AccountService } from './../../account/account.service';
import { IUser } from './../../shared/models/user';
import { ICart } from './../../shared/models/cart';
import { Observable } from 'rxjs';
import { CartService } from './../../cart/cart.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  cart$: Observable<ICart>;
  applicationUser$: Observable<IUser>;

  constructor(private cartService: CartService, private accountService: AccountService) { }

  ngOnInit(): void {
    this.cart$ = this.cartService.cart$;
    this.applicationUser$ = this.accountService.applicationUser$;
  }

  logout() {
    this.accountService.logout();
  }

}
