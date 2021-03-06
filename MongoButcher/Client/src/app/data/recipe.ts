import {Product} from './product';
import {Resource} from './resource';

export class Recipe {
  id ?: string;
  name ?: string;
  procedure ?: string;
  endProduct ?: Product;
  incrediants ?: Array<Resource>;

  constructor(id?: string, name?: string, procedure?: string, output?: Product, ingredients?: Array<Resource>) {
    this.id = id;
    this.name = name;
    this.procedure = procedure;
    this.incrediants = ingredients;
    this.endProduct = output;
  }
}
