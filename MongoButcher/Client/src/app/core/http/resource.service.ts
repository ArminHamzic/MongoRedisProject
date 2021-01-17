import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable, of} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Product} from '../../data/product';
import {GenericService} from './generic/generic-service';
import {Resource} from '../../data/resource';


@Injectable({
  providedIn: 'root'
})
export class ResourceService extends GenericService<Resource, string> {

  $resources = new BehaviorSubject<Resource[]>([]);

  constructor(protected httpClient: HttpClient) {
    super(httpClient, `${environment.api_url}/resource`);
  }

  getResourcesById(id: string): Observable<Resource | any> {
    return this.httpClient.get<Product>(`${this.base}/${id}`);
  }

  loadResources(filter?: string): void {
    if (filter != null) {
      this.getAll().subscribe(e => this.$resources.next(e.filter(p => p.product?.category === filter)));
    } else {
      this.getAll().subscribe(e => this.$resources.next(e));
    }
  }
}
