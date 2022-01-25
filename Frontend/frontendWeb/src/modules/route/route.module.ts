import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouteRoutingModule } from './route-routing.module';
import { ShowAllRoutesComponent } from './show-all-routes/show-all-routes.component';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';
import { DialogOptionRouteComponent } from './dialog-option-route/dialog-option-route.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ManualRouteComponent } from './manual-route/manual-route.component';
import { MatDividerModule } from '@angular/material/divider';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DialogShowRouteComponent } from './dialog-show-route/dialog-show-route.component';
import { GoogleChartsModule } from 'angular-google-charts';
import { AgmCoreModule } from '@agm/core';
import { DialogAutomaticRouteComponent } from './dialog-automatic-route/dialog-automatic-route.component';

@NgModule({
  declarations: [
    ShowAllRoutesComponent,
    DialogOptionRouteComponent,
    ManualRouteComponent,
    DialogShowRouteComponent,
    DialogAutomaticRouteComponent
  ],
  imports: [
    MatTabsModule,
    CommonModule,
    RouteRoutingModule,
    MatIconModule,
    MatDialogModule,
    MatFormFieldModule,
    MatDividerModule,
    ReactiveFormsModule,
    FormsModule,
    GoogleChartsModule,
    AgmCoreModule.forRoot({
      apiKey:'<<GOOGLE KEY>>'
    })
  ]
})
export class RouteModule { }
