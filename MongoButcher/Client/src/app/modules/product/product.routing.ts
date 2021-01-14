import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {ProductComponent} from './page/product/product.component';
import {ProductDetailsComponent} from './page/product-details/product-details.component';
import {ProductResolver} from './product-resolver.service';

export const routes: Routes = [
  {
    path: '',
    component: ProductComponent
  },
  {
    path: 'details/:id',
    component: ProductDetailsComponent,
    resolve: {
      product: ProductResolver
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductRouting {}
