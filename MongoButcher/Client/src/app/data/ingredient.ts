import {Product} from './product';

export class Ingredient {
  id ?: string;
  product ?: Product;
  unit ?: string;
  amount ?: number;

  constructor(id?: string, product?: Product, unit?: string, amount ?: number) {
    this.id = id;
    this.product = product;
    this.unit = unit;
    this.amount = amount;
  }
}
