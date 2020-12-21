import { NgModule } from '@angular/core';
import { ActionHistoryComponent } from './page/action-history/action-history.component';
import {SharedModule} from '../../shared/shared.module';
import {ActionHistoryRouting} from './action-history.routing';



@NgModule({
  declarations: [ActionHistoryComponent],
  imports: [SharedModule, ActionHistoryRouting],
  exports: [],
  providers: [],
  entryComponents: [ActionHistoryComponent]
})
export class ActionHistoryModule { }
