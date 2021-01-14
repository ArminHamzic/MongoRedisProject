import {Product} from './product';
import {Ingredient} from './ingredient';

export class Recipe {
  id ?: string;
  name ?: string;
  procedure ?: string;
  output ?: Product;
  ingredients ?: Array<Ingredient>;

  constructor(id?: string, name?: string, procedure?: string, output?: Product, ingredients?: Array<Ingredient>) {
    this.id = id;
    this.name = name;
    this.name = name;
    this.procedure = procedure;
    this.ingredients = ingredients;
  }
}
