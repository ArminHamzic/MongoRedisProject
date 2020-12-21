import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {ContentLayoutComponent} from './layout/content-layout/content-layout.component';

const routes: Routes = [
  {
    path: '',
    component: ContentLayoutComponent,
    // canActivate: [NoAuthGuard], // Should be replaced with actual auth guard
    children: [
      {
        path: 'dashboard',
        loadChildren: () =>
          import('./modules/home/home.module').then(m => m.HomeModule)
      },
      {
        path: 'products',
        loadChildren: () =>
          import('./modules/product/product.module').then(m => m.ProductModule)
      },
      {
        path: 'action-history',
        loadChildren: () =>
          import('./modules/action-history/action-history.module').then(m => m.ActionHistoryModule)
      },
      {
        path: 'recipes',
        loadChildren: () =>
          import('./modules/recipe/recipe.module').then(m => m.RecipeModule)
      },
      {
        path: 'production',
        loadChildren: () =>
          import('./modules/production/production.module').then(m => m.ProductionModule)
      }
    ]
  }
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
