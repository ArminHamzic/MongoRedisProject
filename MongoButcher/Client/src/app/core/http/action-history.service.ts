import { Injectable } from '@angular/core';
import {GenericService} from './generic/generic-service';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ActionHistoryService extends GenericService<, string>{

  constructor(protected httpClient: HttpClient) {
    super(httpClient, `${environment.api_url}/actionHistory`);
  }

}
