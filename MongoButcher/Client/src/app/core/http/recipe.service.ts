import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable, of} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Recipe} from '../../data/recipe';
import {GenericService} from './generic/generic-service';

const recipeData = [
  new Recipe('1', 'Salt'),
  new Recipe('2', 'Raw Meat'),
  new Recipe('3', 'Peper'),
  new Recipe('4', 'Leber'),
  new Recipe('5', 'Sausage'),
];


@Injectable({
  providedIn: 'root'
})
export class RecipeService extends GenericService<Recipe, string> {

  $recipes = new BehaviorSubject<Recipe[]>(recipeData);

  constructor(protected httpClient: HttpClient) {
    super(httpClient, `${environment.api_url}/recipe`);
  }

  load(): void {
      this.$recipes.next(recipeData);
  }
}
