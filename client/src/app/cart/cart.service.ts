import { IProduct } from 'src/app/shared/models/product';
import { map } from 'rxjs/operators';
import { ICart, ICartItem, Cart, ICartTotals } from './../shared/models/cart';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  baseUrl = environment.apiUrl;

  private cartSource = new BehaviorSubject<ICart>(null);
  cart$ = this.cartSource.asObservable(); // subscribed by several components by async pipe
  
  private cartTotalSource = new BehaviorSubject<ICartTotals>(null);
  cartTotal$ = this.cartTotalSource.asObservable();

  constructor(private http: HttpClient) { }

  getCurrentCart() {
    return this.cartSource.value;
  }

  getCart(id: string) {
    return this.http.get(this.baseUrl + 'cart?id=' + id).pipe(
      map((response: ICart) => {
        this.cartSource.next(response);
        this.getTotals();
      })
    );
  }

  updateCart(cart: ICart) {
    return this.http.post(this.baseUrl + 'cart', cart).subscribe((response: ICart) => {
      this.cartSource.next(response);
      this.getTotals();
    }, error => {
      console.log(error);
    });
  }

  removeCart(cart: ICart) {
    return this.http.delete(this.baseUrl + 'cart?id=' + cart.id).subscribe(() => {
      this.cartSource.next(null);
      this.cartTotalSource.next(null);
      localStorage.removeItem('cart');
    }, error => {
      console.log(error);
    });
  }

  addToCart(product: IProduct, quantity = 1) {
    const cartItem: ICartItem = this.mapToCartItem(product, quantity);
    const cart = this.getCurrentCart() ?? this.createCart(); // get current cart and if null create new cart
    this.addOrUpdateCartItem(cart.cartItems, cartItem);
    this.updateCart(cart);
  }

  incrementQuantity(cartItem: ICartItem) {
    const cart = this.getCurrentCart();
    const index = cart.cartItems.findIndex(x => x.id === cartItem.id);
    cart.cartItems[index].quantity++;
    this.updateCart(cart);
  }

  decrementQuantity(cartItem: ICartItem) {
    const cart = this.getCurrentCart();
    const index = cart.cartItems.findIndex(x => x.id === cartItem.id);
    if (cart.cartItems[index].quantity > 1) {
      cart.cartItems[index].quantity--;
      this.updateCart(cart);
    } else {
      this.removeCartItem(cartItem);
    }
  }

  removeCartItem(cartItem: ICartItem) {
    const cart = this.getCurrentCart();
    if (cart.cartItems.some(x => x.id === cartItem.id)) {
      cart.cartItems = cart.cartItems.filter(x => x.id !== cartItem.id);
      if (cart.cartItems.length > 0) {
        this.updateCart(cart);
      } else {
        this.removeCart(cart);
      }
    }
  }

  private addOrUpdateCartItem(cartItems: ICartItem[], cartItem: ICartItem) {
    const index = cartItems.findIndex(x => x.id === cartItem.id);
    if (index === -1) {
      cartItems.push(cartItem);
    } else {
      cartItems[index].quantity += cartItem.quantity;
    }
  }

  private createCart(): ICart {
    const cart = new Cart();
    localStorage.setItem('cart_id', cart.id); // browser-specific persistence of cart available at application startup
    return cart;
  }

  private mapToCartItem(product: IProduct, quantity: number): ICartItem {
    return {
      id: product.id,
      productName: product.name,
      price: product.price,
      pictureUrl: product.pictureUrl,
      brand: product.productBrand,
      type: product.productType,
      quantity
    };
  }

  private getTotals() {
    const cart = this.getCurrentCart();
    const shipping = 0;
    const subtotal = cart.cartItems.reduce((a, b) => (b.price * b.quantity) + a, 0);
    const total = shipping + subtotal;
    this.cartTotalSource.next({ shipping, total, subtotal });
  }
}
