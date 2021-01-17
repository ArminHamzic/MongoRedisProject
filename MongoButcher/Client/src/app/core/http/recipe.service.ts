import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable, of} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Recipe} from '../../data/recipe';
import {GenericService} from './generic/generic-service';


@Injectable({
  providedIn: 'root'
})
export class RecipeService extends GenericService<Recipe, string> {

  $recipes = new BehaviorSubject<Recipe[]>([]);

  constructor(protected httpClient: HttpClient) {
    super(httpClient, `${environment.api_url}/recipe`);
  }

  load(): void {
    this.getAll().subscribe(e => this.$recipes.next(e));
  }

  produce(recipe: string | undefined): void{
    this.httpClient.get(`${this.base}/execute/${recipe}`).subscribe();
  }
}
