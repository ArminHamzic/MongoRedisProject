import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {RecipeComponent} from './page/recipe/recipe.component';
import {RecipeDetailComponent} from './page/recipe-detail/recipe-detail.component';
import {RecipeResolver} from './recipe-resolver.service';
import {RecipeAddComponent} from './page/recipe-add/recipe-add.component';

export const routes: Routes = [
  {
    path: '',
    component: RecipeComponent
  },
  {
    path: 'details/:id',
    component: RecipeDetailComponent,
    resolve: {
      recipe: RecipeResolver
    }
  },
  {
    path: 'add',
    component: RecipeAddComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RecipeRouting {}
