import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable, of} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Product} from '../../data/product';
import {GenericService} from './generic/generic-service';
import {Resource} from '../../data/resource';

const productData = [
  new Resource('5099803df3f4948bd2f98391', new Product('5099803df3f4948bd2f98391', 'Salt', 'Other Product', 'Kg'), 20),
  new Resource('5099803df3f4945gh2f98394', new Product('5099803df3f4945gh2f98394', 'Raw Meat', 'Raw Product', 'Kg'), 50),
  new Resource('4099803df3f4948er2f98342', new Product('4099803df3f4948er2f98342', 'Peper', 'Other Product', 'Kg'), 10),
  new Resource('5099345df3sdf948bdff9891', new Product('5099345df3sdf948bdff9891', 'Leber', 'Raw Product', 'Kg'), 5),
  new Resource('5099345df3sdf948bdff9351', new Product('5099345df3sdf948bdff9351', 'Sausage', 'Selling Product', 'Stück'), 40),
];

@Injectable({
  providedIn: 'root'
})
export class ResourceService extends GenericService<Resource, string> {

  $resources = new BehaviorSubject<Resource[]>(productData);

  constructor(protected httpClient: HttpClient) {
    super(httpClient, `${environment.api_url}/product`);
  }

  getResourcesById(id: string): Observable<Resource | any> {
    // return this.httpClient.get<Product>(`${this.base}/${id}`);
    return of(productData.find(p => p.id === id));
  }

  loadResources(filter?: string): void {
    if (filter != null) {
      this.$resources.next(productData.filter(p => p?.product?.category === filter));
    } else {
      this.$resources.next(productData);
    }
  }
}
