import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {RecipeComponent} from './page/recipe/recipe.component';

export const routes: Routes = [
  {
    path: '',
    component: RecipeComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RecipeRouting {}
