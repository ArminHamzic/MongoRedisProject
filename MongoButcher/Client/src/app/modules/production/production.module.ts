import { NgModule } from '@angular/core';
import {SharedModule} from '../../shared/shared.module';
import { ProductionComponent } from './page/production/production.component';
import {ProductionRouting} from './production.routing';

@NgModule({
  declarations: [ProductionComponent],
  imports: [SharedModule, ProductionRouting],
  exports: [],
  providers: [],
  entryComponents: [ProductionComponent]
})
export class ProductionModule { }
