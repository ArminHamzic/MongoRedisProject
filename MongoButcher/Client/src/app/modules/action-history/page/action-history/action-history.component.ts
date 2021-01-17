import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {ActionHistory} from '../../../../data/actionHistory';
import {ActionService} from '../../../../core/http/action.service';

@Component({
  selector: 'app-action-history',
  templateUrl: './action-history.component.html',
  styleUrls: ['./action-history.component.scss']
})
export class ActionHistoryComponent implements AfterViewInit {

  constructor(private actionService: ActionService) {
    this.actionService.$actions.subscribe((action) => {
      this.dataSource = new MatTableDataSource(action);
    });
    actionService.load();
  }

  displayedColumns: string[] = ['description', 'time'];
  dataSource!: MatTableDataSource<ActionHistory>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
}
