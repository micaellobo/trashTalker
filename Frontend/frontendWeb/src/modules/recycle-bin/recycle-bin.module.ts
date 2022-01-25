import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RecycleBinRoutingModule } from './recycle-bin-routing.module';
import { AddRecycleBinComponent } from './add-recycle-bin/add-recycle-bin.component';
import { DisableRecycleBinComponent } from './disable-recycle-bin/disable-recycle-bin.component';
import { UpdateRecycleBinComponent } from './update-recycle-bin/update-recycle-bin.component';
import { ShowAllRecycleBinComponent } from './show-all-recycle-bin/show-all-recycle-bin.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatStepperModule } from '@angular/material/stepper';
import { AlertService } from 'src/services/login/alert/alert.service';
import { GoogleChartsModule } from 'angular-google-charts';
import { AgmCoreModule } from '@agm/core';

@NgModule({
  declarations: [
    AddRecycleBinComponent,
    DisableRecycleBinComponent,
    ShowAllRecycleBinComponent,
    UpdateRecycleBinComponent
  ],
  imports: [
    CommonModule,
    CommonModule,
    RecycleBinRoutingModule,
    CommonModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatIconModule,
    MatDialogModule,
    MatStepperModule,
    FormsModule,
    GoogleChartsModule,
    AgmCoreModule.forRoot({
      apiKey:'<<GOOGLE KEY>>'
    })
  ],
  providers:[
    AlertService
  ]
})
export class RecycleBinModule { }
