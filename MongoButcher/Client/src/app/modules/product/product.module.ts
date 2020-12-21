import { NgModule } from '@angular/core';
import { ProductComponent } from './page/product/product.component';
import {SharedModule} from '../../shared/shared.module';
import {ProductRouting} from './product.routing';
import { ProductDetailsComponent } from './page/product-details/product-details.component';
import { ProductCreateComponent } from './page/product-create/product-create.component';

@NgModule({
  declarations: [ProductComponent, ProductDetailsComponent, ProductCreateComponent],
  imports: [SharedModule, ProductRouting],
  exports: [],
  providers: [],
  entryComponents: [ProductComponent]
})
export class ProductModule { }
