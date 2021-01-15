import {Component, OnInit, ViewChild} from '@angular/core';
import {Recipe} from '../../../../data/recipe';
import {MatTableDataSource} from '@angular/material/table';
import {Product} from '../../../../data/product';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatDialog} from '@angular/material/dialog';
import {ResourceService} from '../../../../core/http/resource.service';
import {ProductService} from '../../../../core/http/product.service';
import {ActivatedRoute, Router} from '@angular/router';
import {map} from "rxjs/operators";
import {Observable} from "rxjs";

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrls: ['./recipe-detail.component.scss']
})
export class RecipeDetailComponent implements OnInit {

  displayedColumns: string[] = ['name', 'category', 'unit', 'details'];
  dataSource!: MatTableDataSource<Product>;

  recipe: Recipe;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private resourceService: ResourceService,
              private productService: ProductService,
              private router: Router,
              private route: ActivatedRoute,
              public dialog: MatDialog) {
    this.recipe = new Recipe();
    this.resourceService.$resources.subscribe((resources) => {
      this.dataSource = new MatTableDataSource(resources);
    });
  }

  ngOnInit(): void {
    this.route.data
      .pipe(map(data => data.recipe))
      .subscribe((recipe: Recipe) => {
        this.recipe = recipe;
      });
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

}