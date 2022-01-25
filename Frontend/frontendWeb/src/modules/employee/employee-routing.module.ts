import {NgModule} from "@angular/core";
import {RouterModule, Routes} from "@angular/router";
import {AddComponent} from "./add/add.component";
import {ShowAllComponent} from "./show-all/show-all.component";
import {AuthGuardService} from "../../services/login/auth-guard/auth-guard.service";

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuardService],
    component: ShowAllComponent
  },
  {
    path: 'add',
    canActivate: [AuthGuardService],
    component: AddComponent
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeeRoutingModule {
}
