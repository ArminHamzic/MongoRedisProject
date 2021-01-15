import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {MatSort} from '@angular/material/sort';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import {Product} from '../../../../data/product';
import {ResourceService} from '../../../../core/http/resource.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements AfterViewInit {

  displayedColumns: string[] = ['picture', 'name', 'category', 'amount', 'details'];
  dataSource!: MatTableDataSource<Product>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private resourceService: ResourceService) {
    const test = new Product();
    test.id = '12';
    test.name = 'Salz';
    test.category = 'sonstig';
    this.resourceService.$resources.subscribe((resources) => {
      this.dataSource = new MatTableDataSource(resources);
    });
  }

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

  applyCategoryFilter(filter?: string): void {
    this.resourceService.loadResources(filter);
  }
}
