import { Component, OnInit } from '@angular/core';
import {Product} from '../../../../data/product';
import {Category} from '../../../../data/category';
import {Resource} from '../../../../data/resource';

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.scss']
})
export class ProductCreateComponent implements OnInit {

  resource = new Resource('', new Product(),0);
  categories = Array<Category>();
  constructor() {
  }

  ngOnInit(): void {
  }

  onCreateNewCategory(): void {

  }

  onClose() {

  }

  onSubmit() {

  }
}
