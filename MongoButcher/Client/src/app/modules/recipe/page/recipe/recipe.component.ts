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
  @ViewChild(MatSort) sort!: MatSort;
  ngAfterViewInit(): void {
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



}
