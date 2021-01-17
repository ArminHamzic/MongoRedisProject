import { Component, OnInit } from '@angular/core';
import {Resource} from "../../../../data/resource";
import {MatDialogRef} from "@angular/material/dialog";
import {RecipeAddComponent} from "../recipe-add/recipe-add.component";

@Component({
  selector: 'app-recipe-produce-amount',
  templateUrl: './recipe-produce-amount.component.html',
  styleUrls: ['./recipe-produce-amount.component.scss']
})
export class RecipeProduceAmountComponent implements OnInit {

  amount: number;

  constructor(public dialogRef: MatDialogRef<RecipeAddComponent>) {
    this.amount = 1;
  }

  ngOnInit(): void {
  }

  onClose(): void {
    this.dialogRef.close();
  }

  // @ts-ignore
  async onSubmit(): Resource {
    if (this.amount !== 0) {
      this.dialogRef.close({data: this.amount});
    }
  }

}
