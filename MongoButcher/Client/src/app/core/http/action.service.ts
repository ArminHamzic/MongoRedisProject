import { Injectable } from '@angular/core';
import {GenericService} from './generic/generic-service';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {ActionHistory} from '../../data/actionHistory';
import {BehaviorSubject} from 'rxjs';



@Injectable({
  providedIn: 'root'
})
export class ActionService extends GenericService<ActionHistory, string>{
  $actions = new BehaviorSubject<ActionHistory[]>([]);
  constructor(protected httpClient: HttpClient) {
    super(httpClient, `${environment.api_url}/actionHistory`);
  }
  load(): void {
    this.getAll().subscribe(e => this.$actions.next(e));
  }

}
