import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AddRecycleBinComponent} from './add-recycle-bin/add-recycle-bin.component';
import {ShowAllRecycleBinComponent} from './show-all-recycle-bin/show-all-recycle-bin.component';
import {AuthGuardService} from "../../services/login/auth-guard/auth-guard.service";

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuardService],
    component: ShowAllRecycleBinComponent
  },
  {
    path: 'add',
    canActivate: [AuthGuardService],
    component: AddRecycleBinComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RecycleBinRoutingModule {
}
