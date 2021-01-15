import { Component, OnInit } from '@angular/core';
import {Resource} from '../../../../data/resource';
import {MatDialogRef} from '@angular/material/dialog';
import {RecipeAddComponent} from '../recipe-add/recipe-add.component';

@Component({
  selector: 'app-ingredient-add',
  templateUrl: './ingredient-add.component.html',
  styleUrls: ['./ingredient-add.component.scss']
})
export class IngredientAddComponent implements OnInit {

  resource: Resource;

  constructor(public dialogRef: MatDialogRef<RecipeAddComponent>) {
    this.resource = new Resource();
  }

  ngOnInit(): void {
  }

  onClose(): void {
    this.dialogRef.close();
  }

  // tslint:disable-next-line:typedef
  onSubmit() {

  }
}
