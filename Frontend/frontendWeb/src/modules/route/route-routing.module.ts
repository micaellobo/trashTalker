import {NgModule} from "@angular/core";
import {RouterModule, Routes} from "@angular/router";
import {ManualRouteComponent} from "./manual-route/manual-route.component";
import {ShowAllRoutesComponent} from "./show-all-routes/show-all-routes.component";
import {AuthGuardService} from "../../services/login/auth-guard/auth-guard.service";

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuardService],
    component: ShowAllRoutesComponent
  },
  {
    path: 'manual',
    canActivate: [AuthGuardService],
    component: ManualRouteComponent
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RouteRoutingModule {
}
