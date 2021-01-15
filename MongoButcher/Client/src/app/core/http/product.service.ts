import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable, of} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Product} from '../../data/product';
import {GenericService} from './generic/generic-service';

const productData = [
  new Product('5099803df3f4948bd2f98391', 'Salt', 'Other Product', 'Kg'),
  new Product('5099803df3f4945gh2f98394', 'Raw Meat', 'Raw Product','Kg'),
  new Product('4099803df3f4948er2f98342', 'Peper', 'Other Product', 'Kg'),
  new Product('5099345df3sdf948bdff9891', 'Leber', 'Raw Product', 'Kg'),
  new Product('5099345df3sdf948bdff9351', 'Sausage', 'Selling Product','Stück'),
];

@Injectable({
  providedIn: 'root'
})
export class ProductService extends GenericService<Product, string> {

  $products = new BehaviorSubject<Product[]>(productData);

  constructor(protected httpClient: HttpClient) {
    super(httpClient, `${environment.api_url}/product`);
  }

  getProductById(id: string): Observable<Product | any> {
    // return this.httpClient.get<Product>(`${this.base}/${id}`);
    return of(productData.find(p => p.id === id));
  }

  loadProducts(filter?: string): void {
    if (filter != null) {
      this.$products.next(productData.filter(p => p.category === filter));
    } else {
      this.$products.next(productData);
    }
  }
}
