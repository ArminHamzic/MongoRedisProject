import {Observable} from 'rxjs';

export interface CrudOperations<T, ID> {
  getById(id: ID): Observable<T>;
  getAll(): Observable<T[]>;
  save(t: T): Observable<T>;
  update(id: ID, t: T): Observable<T>;
  delete(id: ID): Observable<any>;
}
