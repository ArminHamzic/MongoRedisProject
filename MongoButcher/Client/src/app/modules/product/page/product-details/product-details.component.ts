import { Component, OnInit } from '@angular/core';
import {Observable} from 'rxjs';
import {Product} from '../../../../data/product';
import {ActivatedRoute} from '@angular/router';
import {map} from 'rxjs/operators';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product$!: Observable<Product>;

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.product$ = this.route.data.pipe(map(data => data.product));
  }

}
