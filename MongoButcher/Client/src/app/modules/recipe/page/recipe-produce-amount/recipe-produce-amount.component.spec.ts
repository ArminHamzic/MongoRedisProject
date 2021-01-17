import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipeProduceAmountComponent } from './recipe-produce-amount.component';

describe('RecipeProduceAmountComponent', () => {
  let component: RecipeProduceAmountComponent;
  let fixture: ComponentFixture<RecipeProduceAmountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecipeProduceAmountComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RecipeProduceAmountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
