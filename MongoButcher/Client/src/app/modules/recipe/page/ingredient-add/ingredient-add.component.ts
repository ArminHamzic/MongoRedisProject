import { Component, OnInit } from '@angular/core';
import {Resource} from '../../../../data/resource';
import {MatDialogRef} from '@angular/material/dialog';
import {RecipeAddComponent} from '../recipe-add/recipe-add.component';
import {Product} from '../../../../data/product';
import {ProductService} from '../../../../core/http/product.service';

@Component({
  selector: 'app-ingredient-add',
  templateUrl: './ingredient-add.component.html',
  styleUrls: ['./ingredient-add.component.scss']
})
export class IngredientAddComponent implements OnInit {

  resource: Resource;
  products: Product[] = [];

  constructor(public dialogRef: MatDialogRef<RecipeAddComponent>,
              public productService: ProductService) {
    this.resource = new Resource();
    this.productService.$products.subscribe((prod) => {
      this.products = prod;
    });
    this.productService.loadProducts();
  }

  ngOnInit(): void {
  }

  onClose(): void {
    this.dialogRef.close();
  }

  // @ts-ignore
  async onSubmit(): Resource {
    if (this.resource != null) {
      this.dialogRef.close({data: this.resource});
    }
  }
}
