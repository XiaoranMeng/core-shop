import { CartService } from './../../cart/cart.service';
import { ShopService } from './../shop.service';
import { IProduct } from './../../shared/models/product';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  quantity = 1;
  counter = 0;

  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private breadCrumbService: BreadcrumbService,
    private cartService: CartService) {
      this.breadCrumbService.set('@productDetails', '');
  }

  ngOnInit(): void {
    this.getProduct();
  }

  getProduct() {
    const id = +this.activatedRoute.snapshot.paramMap.get('id');
    this.shopService.getProduct(id).subscribe(response => {
      this.product = response;
      this.breadCrumbService.set('@productDetails', response.name);
    }, error => {
      console.log(error);
    });
  }

  addToCart() {
    this.cartService.addToCart(this.product, this.quantity);
  }

  incrementQuantity() {
    this.quantity++;
    this.counter = 0;
  }

  decrementQuantity() {
    if (this.quantity > 1) {
      this.quantity--;
    } else {
      this.counter--;
    }
  }
}
