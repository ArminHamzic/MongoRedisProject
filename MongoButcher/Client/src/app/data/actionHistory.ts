import {Product} from './product';

export class ActionHistory {
  id ?: string;
  description ?: string;
  creationDate ?: Date;

  constructor(id?: string, description?: string, creationDate?: Date) {
    this.id = id;
    this.description = description;
    this.creationDate = creationDate;
  }
}
