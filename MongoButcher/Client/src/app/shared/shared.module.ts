import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


import { ControlMessagesComponent } from './component/control-messages/control-messages.component';
import {MaterialModule} from './material.module';
import {SpinnerComponent} from './component/spinner/spinner.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,

    NgbModule
  ],
  declarations: [ControlMessagesComponent, SpinnerComponent],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,

    MaterialModule,

    NgbModule,

    ControlMessagesComponent,
    SpinnerComponent
  ]
})
export class SharedModule {}
