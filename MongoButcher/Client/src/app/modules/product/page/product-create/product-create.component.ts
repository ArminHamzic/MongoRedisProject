import { Component, OnInit } from '@angular/core';
import {Product} from '../../../../data/product';
import {Category} from '../../../../data/category';
import {CategoryService} from '../../../../core/http/category.service';
import {ProductService} from '../../../../core/http/product.service';
import {Router} from '@angular/router';

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

  constructor(private productService: ProductService,
              private categoryService: CategoryService,
              private router: Router) {
  }

  ngOnInit(): void {
    this.categoryService.$category.subscribe(categories => {
      this.categories = categories;
    });
    this.categoryService.loadCategories();
  }

  onClose(): void  {
    this.router.navigate(['/products']);
  }

  onSubmit(): void {
    this.product.category = this.category;
    this.productService.save(this.product).subscribe(e => {
      this.router.navigate(['/products']);
    });
  }
}
