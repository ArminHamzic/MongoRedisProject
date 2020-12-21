import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable, of} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Product} from '../../data/product';

const productData = [
  new Product('5099803df3f4948bd2f98391', 'Salt', 'Other Product', 20, 'Kg'),
  new Product('5099803df3f4945gh2f98394', 'Raw Meat', 'Raw Product', 50, 'Kg'),
  new Product('4099803df3f4948er2f98342', 'Peper', 'Other Product', 10, 'Kg'),
  new Product('5099345df3sdf948bdff9891', 'Leber', 'Raw Product', 5, 'Kg'),
  new Product('5099345df3sdf948bdff9351', 'Sausage', 'Selling Product', 40, 'Stück'),
];

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  baseUrl: string;

  $products = new BehaviorSubject<Product[]>(productData);

  constructor(private httpClient: HttpClient) {
    this.baseUrl = environment.api_url;
  }

  getProductById(id: string): Observable<Product | any> {
    // return this.httpClient.get<Product>(`${this.baseUrl}/api/products/${id}`);
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
