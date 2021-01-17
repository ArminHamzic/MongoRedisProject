import { Component, OnInit } from '@angular/core';
import {Product} from '../../../../data/product';
import {Category} from '../../../../data/category';
import {Resource} from '../../../../data/resource';
import {ResourceService} from '../../../../core/http/resource.service';

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.scss']
})
export class ProductCreateComponent implements OnInit {

  resource = new Resource('', new Product(), 0);

  category = new Category('Spices', 'Includes all different spices', 'Kg');
  categories = Array<Category>();
  constructor(private resourceService: ResourceService) {
  }

  ngOnInit(): void {
  }

  onCreateNewCategory(): void {

  }

  onClose(): void  {

  }

  onSubmit(): void {
    this.resource.product.category = this.category;
    this.resourceService.save(this.resource).subscribe();
  }
}
