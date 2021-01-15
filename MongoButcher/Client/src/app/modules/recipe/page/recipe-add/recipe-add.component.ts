import { Component, OnInit } from '@angular/core';
import {Recipe} from '../../../../data/recipe';

@Component({
  selector: 'app-recipe-add',
  templateUrl: './recipe-add.component.html',
  styleUrls: ['./recipe-add.component.scss']
})
export class RecipeAddComponent implements OnInit {

  constructor() {
    this.recipe = new Recipe();
  }

  recipe: Recipe;

  ngOnInit(): void {
  }

}
