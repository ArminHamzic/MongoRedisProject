export class Category {
  id?: string;
  name?: string;
  description?: string;
  defaultUnit?: string;

  constructor(name?: string, description?: string, defaultUnit?: string) {
    this.name = name;
    this.description = description;
    this.defaultUnit = defaultUnit;
  }
}
