import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeRoutingModule } from './employee-routing.module';
import { ShowAllComponent } from './show-all/show-all.component';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { DisableComponent } from './disable/disable.component';
import { MatDialogModule } from '@angular/material/dialog';
import { AddComponent } from './add/add.component';
import {MatStepperModule} from '@angular/material/stepper';
import { FormsModule } from '@angular/forms';
import { UpdateComponent } from './update/update.component';
import { AlertService } from 'src/services/login/alert/alert.service';
@NgModule({
  declarations: [
    ShowAllComponent,
    DisableComponent,
    AddComponent,
    UpdateComponent
  ],
  imports: [
    CommonModule,
    EmployeeRoutingModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatIconModule,
    MatDialogModule,
    MatStepperModule,
    FormsModule
  ],
  providers: [
    AlertService
  ],
  exports:[
    AddComponent
  ]
})
export class EmployeeModule { }
