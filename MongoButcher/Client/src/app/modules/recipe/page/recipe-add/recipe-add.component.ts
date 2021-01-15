import {Component, OnInit, ViewChild} from '@angular/core';
import {Recipe} from '../../../../data/recipe';
import {MatTableDataSource} from '@angular/material/table';
import {Product} from '../../../../data/product';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {ResourceService} from '../../../../core/http/resource.service';

@Component({
  selector: 'app-recipe-add',
  templateUrl: './recipe-add.component.html',
  styleUrls: ['./recipe-add.component.scss']
})
export class RecipeAddComponent implements OnInit {

  displayedColumns: string[] = ['name', 'category', 'amount', 'details', 'search'];
  dataSource!: MatTableDataSource<Product>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private resourceService: ResourceService) {
    this.recipe = new Recipe();

    this.resourceService.$resources.subscribe((resources) => {
      this.dataSource = new MatTableDataSource(resources);
    });
  }

  recipe: Recipe;

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

  applyCategoryFilter(filter?: string): void {
    this.resourceService.loadResources(filter);
  }

}
