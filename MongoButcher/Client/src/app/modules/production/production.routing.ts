import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {ProductionComponent} from './page/production/production.component';

export const routes: Routes = [
  {
    path: '',
    component: ProductionComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductionRouting {}
