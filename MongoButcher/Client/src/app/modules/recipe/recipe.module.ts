import { NgModule } from '@angular/core';
import {SharedModule} from '../../shared/shared.module';
import { RecipeComponent } from './page/recipe/recipe.component';
import {RecipeRouting} from './recipe.routing';
import { RecipeDetailComponent } from './page/recipe-detail/recipe-detail.component';
import { RecipeAddComponent } from './page/recipe-add/recipe-add.component';
import { IngredientAddComponent } from './page/ingredient-add/ingredient-add.component';
import { RecipeProduceAmountComponent } from './page/recipe-produce-amount/recipe-produce-amount.component';

@NgModule({
  declarations: [RecipeComponent, RecipeDetailComponent, RecipeAddComponent, IngredientAddComponent, RecipeProduceAmountComponent],
  imports: [SharedModule, RecipeRouting],
  exports: [],
  providers: [],
  entryComponents: [RecipeComponent]
})
export class RecipeModule { }
