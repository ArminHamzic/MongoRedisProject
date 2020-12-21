import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {ActionHistoryComponent} from './page/action-history/action-history.component';

export const routes: Routes = [
  {
    path: '',
    component: ActionHistoryComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ActionHistoryRouting {}
