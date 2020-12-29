import { ShopService } from './../shop.service';
import { IProduct } from './../../shared/models/product';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;

  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.getProduct();
  }

  getProduct() {
    const id = +this.activatedRoute.snapshot.paramMap.get('id');
    this.shopService.getProduct(id).subscribe(response => {
      this.product = response;
    }, error => {
      console.log(error);
    });
  }


}
