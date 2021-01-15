export class Product {
  id ? = '5099803df3f4948bd2f98391';
  name ? = 'Sausage';
  category ? = 'Selling Product';
  picBase64 ?: string;
  unit ? = 'Stück';

  constructor(id?: string, name?: string, category?: string, unit?: string) {
    this.id = id;
    this.name = name;
    this.category = category;
    this.unit = unit;
  }
}
