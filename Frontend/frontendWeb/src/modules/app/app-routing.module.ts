import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AuthGuardService} from 'src/services/login/auth-guard/auth-guard.service';
import {HomeContentComponent} from './home-content/home-content.component';
import {LoginComponent} from './login/login.component';
import {MainPageComponent} from './main-page/main-page.component';
import {AdminGuardGuard} from "../../services/login/admin-guard.guard";
import {AdminSectionComponent} from "./admin-section/admin-section.component";


const routes: Routes = [
  {
    path: '',
    component: LoginComponent
  },
  {
    path: 'admin',
    canActivate: [AdminGuardGuard],
    component: AdminSectionComponent
  },
  {
    path: 'home',
    canActivate: [AuthGuardService],
    component: MainPageComponent,
    children: [
      {
        path: '',
        canActivate: [AuthGuardService],
        component: HomeContentComponent
      },
      {
        path: 'func',
        canActivate: [AuthGuardService],
        loadChildren: () => import('../employee/employee.module').then((m) => m.EmployeeModule)
      },
      {
        path: 'routes',
        canActivate: [AuthGuardService],
        loadChildren: () => import('../route/route.module').then((m) => m.RouteModule)
      },
      {
        path: 'eco',
        canActivate: [AuthGuardService],
        loadChildren: () => import('../recycle-bin/recycle-bin.module').then((m) => m.RecycleBinModule)
      },
      {
        path: 'alerts',
        canActivate: [AuthGuardService],
        loadChildren: () => import('../alerts/alerts.module').then((m) => m.AlertsModule)
      },
      {
        path: 'dashboard',
        canActivate: [AuthGuardService],
        loadChildren: () => import('../dashboard/dashboard.module').then((m) => m.DashboardModule)
      }
    ]
  },

  {path: '**', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
