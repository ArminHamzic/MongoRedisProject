import { NgModule } from '@angular/core';
import {SharedModule} from '../../shared/shared.module';
import { RecipeComponent } from './page/recipe/recipe.component';
import {RecipeRouting} from './recipe.routing';

@NgModule({
  declarations: [RecipeComponent],
  imports: [SharedModule, RecipeRouting],
  exports: [],
  providers: [],
  entryComponents: [RecipeComponent]
})
export class RecipeModule { }
