import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlertsRoutingModule } from './alerts-routing.module';
import { ShowAllAlertsComponent } from './show-all-alerts/show-all-alerts.component';
import { ResolveAlertComponent } from './resolve-alert/resolve-alert.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatStepperModule } from '@angular/material/stepper';
import { AlertsService } from 'src/services/alerts/alerts.service';

@NgModule({
  declarations: [
    ShowAllAlertsComponent,
    ResolveAlertComponent
  ],
  imports: [
    CommonModule,
    AlertsRoutingModule,
    CommonModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatIconModule,
    MatDialogModule,
    MatStepperModule,
    FormsModule
  ],
  providers:[AlertsService]
})
export class AlertsModule { }
