import { Component, OnInit } from '@angular/core';
import {Product} from '../../../../data/product';
import {Category} from '../../../../data/category';
import {Resource} from '../../../../data/resource';
import {ResourceService} from '../../../../core/http/resource.service';
import {CategoryService} from '../../../../core/http/category.service';
import {ProductService} from '../../../../core/http/product.service';

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.scss']
})
export class ProductCreateComponent implements OnInit {

  // resource = new Resource('', new Product(), 0);
  product = new Product();
  category = new Category('Spices', 'Includes all different spices', 'Kg');

  categories = Array<Category>();
  constructor(private productService: ProductService, private categoryService: CategoryService) {
  }

  ngOnInit(): void {
    this.categoryService.$category.subscribe(categories => {
      this.categories = categories;
    });
    this.categoryService.loadCategories();
  }

  onCreateNewCategory(): void {

  }

  onClose(): void  {

  }

  onSubmit(): void {
    this.product.category = this.category;
    this.productService.save(this.product).subscribe(e => {
      // console.log(e);
    });
  }
}
