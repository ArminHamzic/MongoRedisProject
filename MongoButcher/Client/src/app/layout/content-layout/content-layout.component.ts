import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-content-layout',
  templateUrl: './content-layout.component.html',
  styleUrls: ['./content-layout.component.scss']
})
export class ContentLayoutComponent implements OnInit {

  sideBarOpen: boolean;

  constructor() {
    this.sideBarOpen = true;
  }

  ngOnInit(): void {
  }

  sideBarTrigger($event: any): void {
    this.sideBarOpen = !this.sideBarOpen;
  }

}
