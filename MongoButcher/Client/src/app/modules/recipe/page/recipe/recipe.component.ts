import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {Recipe} from '../../../../data/recipe';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {RecipeService} from '../../../../core/http/recipe.service';



@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.scss']
})
export class RecipeComponent implements AfterViewInit {

  constructor(private recipeService: RecipeService) {
    this.recipeService.$recipes.subscribe((recipes) => {
      this.dataSource = new MatTableDataSource(recipes);
    });
  }

  displayedColumns: string[] = ['picture', 'name', 'details'];
  dataSource!: MatTableDataSource<Recipe>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }
}
