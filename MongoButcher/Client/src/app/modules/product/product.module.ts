import { NgModule } from '@angular/core';
import { ProductComponent } from './page/product/product.component';
import {SharedModule} from '../../shared/shared.module';
import {ProductRouting} from './product.routing';

@NgModule({
  declarations: [ProductComponent],
  imports: [SharedModule, ProductRouting],
  exports: [],
  providers: [],
  entryComponents: [ProductComponent]
})
export class ProductModule { }
