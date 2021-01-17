import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {MatSort} from '@angular/material/sort';
import {MatPaginator} from '@angular/material/paginator';
import {MatTableDataSource} from '@angular/material/table';
import {ResourceService} from '../../../../core/http/resource.service';
import {Resource} from '../../../../data/resource';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements AfterViewInit {

  displayedColumns: string[] = ['picture', 'name', 'category', 'amount', 'details'];
  dataSource!: MatTableDataSource<Resource>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  amount: number = 0;

  constructor(private resourceService: ResourceService) {
    this.resourceService.$resources.subscribe((resources) => {
      this.dataSource = new MatTableDataSource(resources);
    });
    this.resourceService.loadResources();
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

  addAmount(row: Resource): void {
    // @ts-ignore
    row.amount += this.amount;
    row.actionHistories = [];
    this.resourceService.update(row).subscribe(e => this.resourceService.loadResources());
    this.amount = 0;
  }
}
