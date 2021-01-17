import {Component, OnInit, ViewChild} from '@angular/core';
import {Recipe} from '../../../../data/recipe';
import {MatTableDataSource} from '@angular/material/table';
import {Product} from '../../../../data/product';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatDialog} from '@angular/material/dialog';
import {IngredientAddComponent} from '../ingredient-add/ingredient-add.component';
import {ResourceService} from '../../../../core/http/resource.service';
import {ProductService} from '../../../../core/http/product.service';
import {RecipeService} from '../../../../core/http/recipe.service';
import {Resource} from '../../../../data/resource';

@Component({
  selector: 'app-recipe-add',
  templateUrl: './recipe-add.component.html',
  styleUrls: ['./recipe-add.component.scss']
})
export class RecipeAddComponent implements OnInit {

  displayedColumns: string[] = ['name', 'category', 'unit', 'details', 'addResource', 'search'];
  dataSource!: MatTableDataSource<Resource>;
  recipe: Recipe;
  products: Product[] = [];

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private recipeService: RecipeService,
              public productService: ProductService,
              public dialog: MatDialog) {
    this.recipe = new Recipe();
    this.recipe.incrediants = [];
    this.dataSource = new MatTableDataSource();
    this.productService.$products.subscribe((prod) => {
      this.products = prod;
    });
    this.productService.loadProducts();
  }

  ngOnInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  onAddingResource(): void {
    const dialogRef = this.dialog.open(IngredientAddComponent, {autoFocus: true, width: '20%', disableClose: true});
    dialogRef.afterClosed().subscribe(result => {
      console.log(result.data);
      this.recipe.incrediants?.push(result.data);
      console.log(this.recipe.incrediants);
      this.dataSource = new MatTableDataSource(this.recipe.incrediants);
    });
  }

  onClose(): void {

  }

  onSubmit(): void {
    console.log(this.recipe);
    this.recipeService.save(this.recipe).subscribe();
  }

  onDelete(resource: Resource): void {
    this.recipe.incrediants?.splice(this.recipe.incrediants?.indexOf(resource), 1);
    this.dataSource = new MatTableDataSource(this.recipe.incrediants);
  }
}
