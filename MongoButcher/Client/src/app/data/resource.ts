import {Product} from './product';
import {ActionHistory} from './actionHistory';

export class Resource {
  id ?: string;
  product ?: Product;
  amount ?: number;
  actionHistories ?: Array<ActionHistory>;

  constructor(id?: string, product?: Product, amount ?: number) {
    this.id = id;
    this.product = product;
    this.amount = amount;
    this.actionHistories = [];
  }
}
