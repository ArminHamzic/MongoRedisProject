import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable, of} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {Recipe} from '../../data/recipe';

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
export class RecipeService {

  baseUrl: string;

  $recipes = new BehaviorSubject<Recipe[]>(recipeData);

  constructor(private httpClient: HttpClient) {
    this.baseUrl = environment.api_url;
  }

  getRecipeById(id: string): Observable<Recipe | any> {
    // return this.httpClient.get<Recipe>(`${this.baseUrl}/api/recipe/${id}`);
    return of(recipeData.find(r => r.id === id));
  }

  load(): void {
      this.$recipes.next(recipeData);
  }
}
