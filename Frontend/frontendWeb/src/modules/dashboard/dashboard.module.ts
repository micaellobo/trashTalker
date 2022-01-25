import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardRoutingModule } from '../dashboard/dashboard-routing.module';
import { DashboardShowComponent } from './dashboard-show/dashboard-show.component';
import { GoogleChartsModule } from 'angular-google-charts';
import { Chart1Component } from './chart1/chart1.component';
import { Chart2Component } from './chart2/chart2.component';
import { Chart3Component } from './chart3/chart3.component';
import { Chart4Component } from './chart4/chart4.component';
import { FormsModule } from '@angular/forms';
import { Chart5Component } from './chart5/chart5.component';



@NgModule({
  declarations: [
    DashboardShowComponent,
    Chart1Component,
    Chart2Component,
    Chart3Component,
    Chart4Component,
    Chart5Component
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    GoogleChartsModule,
    FormsModule
  ],
  exports:[DashboardShowComponent]
})
export class DashboardModule { }
