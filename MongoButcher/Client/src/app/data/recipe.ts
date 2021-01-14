import {Product} from './product';
import {Ingredient} from './ingredient';

export class Recipe {
  id ?: string;
  name ?: string;
  output ?: Product;
  ingredients ?: Array<Ingredient>;

  constructor(id?: string, name?: string, output?: Product, ingredients?: Array<Ingredient>) {
    this.id = id;
    this.name = name;
    this.output = output;
    this.ingredients = ingredients;
  }
}
