import {Product} from './product';

export class ActionHistory {
  id ?: string;
  description ?: string;
  creationDate ?: string;

  constructor(id?: string, description?: string, creationDate?: string) {
    this.id = id;
    this.description = description;
    this.creationDate = creationDate;
  }
}
