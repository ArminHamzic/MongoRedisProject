import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable, of} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Product} from '../../data/product';
import {GenericService} from './generic/generic-service';
import {Resource} from '../../data/resource';
import {Category} from '../../data/category';


@Injectable({
  providedIn: 'root'
})
export class CategoryService extends GenericService<Category, string> {

  $category = new BehaviorSubject<Category[]>([]);

  constructor(protected httpClient: HttpClient) {
    super(httpClient, `${environment.api_url}/category`);
  }

  getCategoryById(id: string): Observable<Resource | any> {
    return this.httpClient.get<Category>(`${this.base}/${id}`);
  }

  loadCategories(): void {
    this.getAll().subscribe(e => this.$category.next(e));
  }
}
