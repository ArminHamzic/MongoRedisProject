import {Product} from './product';

export class Action {
  id ?: string;
  description ?: string;
  creationDate ?: Date;

  constructor(id?: string, description?: string, creationDate?: Date) {
    this.id = id;
    this.description = description;
    this.creationDate = creationDate;
  }
}
