import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {ResolveAlertComponent} from './resolve-alert/resolve-alert.component';
import {ShowAllAlertsComponent} from './show-all-alerts/show-all-alerts.component';
import {AuthGuardService} from "../../services/login/auth-guard/auth-guard.service";

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuardService],
    component: ShowAllAlertsComponent
  },
  {
    path: 'resolve',
    canActivate: [AuthGuardService],
    component: ResolveAlertComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AlertsRoutingModule {
}
