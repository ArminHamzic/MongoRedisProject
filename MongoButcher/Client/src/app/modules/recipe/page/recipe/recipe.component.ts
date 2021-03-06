import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {Recipe} from '../../../../data/recipe';
import {MatPaginator} from '@angular/material/paginator';
import {RecipeService} from '../../../../core/http/recipe.service';



@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.scss']
})
export class RecipeComponent implements OnInit {

  constructor(private recipeService: RecipeService) {
  }

  displayedColumns: string[] = ['picture', 'name', 'details'];
  dataSource!: MatTableDataSource<Recipe>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  onDelete(id: string): void {
    this.recipeService.delete(id).subscribe(r => this.recipeService.load());
  }

  ngOnInit(): void {
    this.recipeService.$recipes.subscribe((recipes) => {
      this.dataSource = new MatTableDataSource(recipes);
      console.log(recipes);
    });
    this.recipeService.load();
    this.dataSource.paginator = this.paginator;
  }
}
