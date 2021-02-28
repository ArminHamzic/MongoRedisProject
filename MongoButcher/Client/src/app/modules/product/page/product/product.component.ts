import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
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
export class ProductComponent implements OnInit {

  displayedColumns: string[] = ['picture', 'name', 'category', 'amount', 'details'];
  dataSource!: MatTableDataSource<Resource>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  amounts: number[] = [];

  constructor(private resourceService: ResourceService) {
  }

  ngOnInit(): void {
    this.resourceService.$resources.subscribe((res) => {
      this.dataSource = new MatTableDataSource(res);
    });
    this.resourceService.loadResources();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  addAmount(index: number, row: Resource): void {
    // @ts-ignore
    row.amount += this.amounts[index];
    row.actionHistories = [];
    this.resourceService.update(row).subscribe(e => this.resourceService.loadResources());
    // @ts-ignore
    this.amounts = [];
  }
}
