import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {DashboardShowComponent} from './dashboard-show/dashboard-show.component';
import {AuthGuardService} from "../../services/login/auth-guard/auth-guard.service";

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuardService],
    component: DashboardShowComponent
  }
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),
    CommonModule
  ],
  exports: [RouterModule]
})
export class DashboardRoutingModule {
}
