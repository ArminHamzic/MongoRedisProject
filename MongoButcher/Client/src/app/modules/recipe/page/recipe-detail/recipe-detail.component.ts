import {Component, OnInit, ViewChild} from '@angular/core';
import {Recipe} from '../../../../data/recipe';
import {MatTableDataSource} from '@angular/material/table';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatDialog} from '@angular/material/dialog';
import {ResourceService} from '../../../../core/http/resource.service';
import {ActivatedRoute, Router} from '@angular/router';
import {map} from 'rxjs/operators';
import {Resource} from '../../../../data/resource';
import {IngredientAddComponent} from "../ingredient-add/ingredient-add.component";
import {RecipeProduceAmountComponent} from "../recipe-produce-amount/recipe-produce-amount.component";
import {RecipeService} from '../../../../core/http/recipe.service';

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrls: ['./recipe-detail.component.scss']
})
export class RecipeDetailComponent implements OnInit {

  displayedColumns: string[] = ['name', 'category', 'unit'];
  dataSource!: MatTableDataSource<Resource>;

  recipe: Recipe;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private resourceService: ResourceService,
              private recipeService: RecipeService,
              private router: Router,
              private route: ActivatedRoute,
              public dialog: MatDialog) {
    this.recipe = new Recipe();
  }

  ngOnInit(): void {
    this.route.data
      .pipe(map(data => data.recipe))
      .subscribe((recipe: Recipe) => {
        this.recipe = recipe;
        console.log(recipe.incrediants);
        this.dataSource = new MatTableDataSource(this.recipe.incrediants);
      });
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }


  onProduce(): void {
    const dialogRef = this.dialog.open(RecipeProduceAmountComponent, {autoFocus: true, width: '20%', disableClose: true});
    dialogRef.afterClosed().subscribe(result => {
      console.log(result.data);
      for (let i = 0; i < result.data; i++){
        this.recipeService.produce(this.recipe.name);
        this.resourceService.loadResources();
      }
    });
  }
}
