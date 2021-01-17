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

}
