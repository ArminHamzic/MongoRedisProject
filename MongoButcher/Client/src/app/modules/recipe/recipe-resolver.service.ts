import { Injectable, } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import {RecipeService} from '../../core/http/recipe.service';
import {Recipe} from '../../data/recipe';


@Injectable({
  providedIn: 'root'
})
export class RecipeResolver implements Resolve<Recipe> {
  constructor(
    private recipeService: RecipeService,
    private router: Router
  ) { }

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> {
    return this.recipeService.getRecipeById(route.params.id)
      .pipe(catchError((err) => this.router.navigateByUrl('/')));
  }
}
