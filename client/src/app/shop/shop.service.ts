import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { IType } from '../shared/models/productType';
import { map } from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams()
      .append('brandId', shopParams.brandId.toString())
      .append('typeId', shopParams.typeId.toString())
      .append('orderBy', shopParams.orderBy)
      .append('pageIndex', shopParams.pageIndex.toString())
      .append('pageSize', shopParams.pageSize.toString());

    if (shopParams.search && shopParams.search !== '') {
      params = params.append('search', shopParams.search);
    }

    return this.http.get<IPagination>(this.baseUrl + 'products', {
      observe: 'response',
      params
    }).pipe(
        map(response => {
          return response.body;
        })
    );
  }

  getBrands() {
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }

  getTypes() {
    return this.http.get<IType[]>(this.baseUrl + 'products/types');
  }
}
