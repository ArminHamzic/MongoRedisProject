import { Component, OnInit } from '@angular/core';
import {Resource} from '../../../../data/resource';
import {MatDialogRef} from '@angular/material/dialog';
import {RecipeAddComponent} from '../recipe-add/recipe-add.component';
import {Product} from '../../../../data/product';
import {ProductService} from '../../../../core/http/product.service';
import {MatSnackBar} from '@angular/material/snack-bar';

@Component({
  selector: 'app-ingredient-add',
  templateUrl: './ingredient-add.component.html',
  styleUrls: ['./ingredient-add.component.scss']
})
export class IngredientAddComponent implements OnInit {

  resource: Resource;
  product: Product;
  products: Product[] = [];

  constructor(public dialogRef: MatDialogRef<RecipeAddComponent>,
              public productService: ProductService) {
    this.resource = new Resource();
    this.productService.$products.subscribe((prod) => {
      this.products = prod;
    });
    this.product = new Product();
    this.product.name = 'test';
    this.product.category = 'test';
    this.product.unit = '22';
    this.products.push(this.product);
  }

  ngOnInit(): void {
  }

  onClose(): void {
    this.dialogRef.close();
  }

  // @ts-ignore
  async onSubmit(): Resource {
    if (this.resource != null) {
      this.onClose();
      return this.resource;
    }
  }
}
