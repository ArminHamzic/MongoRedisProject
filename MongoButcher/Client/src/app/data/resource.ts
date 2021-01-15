import {Product} from './product';

export class Resource {
  id ?: string;
  product ?: Product;
  amount ?: number;

  constructor(id?: string, product?: Product, amount ?: number) {
    this.id = id;
    this.product = product;
    this.amount = amount;
  }
}
