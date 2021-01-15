import { Injectable } from '@angular/core';
import {GenericService} from './generic/generic-service';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Action} from '../../data/action';
import {BehaviorSubject} from 'rxjs';
import {Recipe} from '../../data/recipe';

const recipeData = [
  new Action('12313', 'keine Ahnung', Date.prototype),
  new Action('12313', 'imma nu keine Ahnung', Date.prototype),
  new Action('12313', 'i was doch a ned', Date.prototype),
];

@Injectable({
  providedIn: 'root'
})
export class ActionService extends GenericService<Action, string>{
  $actions = new BehaviorSubject<Action[]>(recipeData);
  constructor(protected httpClient: HttpClient) {
    super(httpClient, `${environment.api_url}/actionHistory`);
  }
  load(): void {
    this.$actions.next(recipeData);
  }

}
