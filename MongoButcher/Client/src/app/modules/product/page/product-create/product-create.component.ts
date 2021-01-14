import { Component, OnInit } from '@angular/core';
import {Product} from '../../../../data/product';
import {Category} from '../../../../data/category';

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.scss']
})
export class ProductCreateComponent implements OnInit {

  product = new Product();

  categories = Array<Category>();
  constructor() { }

  ngOnInit(): void {
  }

  onCreateNewCategory(): void {

  }

  onClose() {

  }

  onSubmit() {

  }
}
