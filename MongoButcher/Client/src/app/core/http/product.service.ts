import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable, of} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Product} from '../../data/product';
import {GenericService} from './generic/generic-service';


@Injectable({
  providedIn: 'root'
})
export class ProductService extends GenericService<Product, string> {

  $products = new BehaviorSubject<Product[]>([]);

  constructor(protected httpClient: HttpClient) {
    super(httpClient, `${environment.api_url}/product`);
  }

  getProductById(id: string): Observable<Product | any> {
    return this.httpClient.get<Product>(`${this.base}/${id}`);
  }

  loadProducts(filter?: string): void {
    if (filter != null) {
      this.getAll().subscribe(e => this.$products.next(e.filter(p => p.category === filter)));
    } else {
      this.getAll().subscribe(e => this.$products.next(e));
    }
  }
}
