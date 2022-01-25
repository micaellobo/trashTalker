import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {SideBarComponent} from './shared/side-bar/side-bar.component';
import {ToolbarComponent} from './shared/toolbar/toolbar.component';
import {MainPageComponent} from './main-page/main-page.component';
import {HomeContentComponent} from './home-content/home-content.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatIconModule} from '@angular/material/icon';
import {EmployeeService} from 'src/services/employee/employee.service';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {LoginComponent} from './login/login.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {AgmCoreModule} from '@agm/core';
import {RecyclerBinService} from 'src/services/recyclerBin/recycler-bin.service';
import {DialogInfoRecBinComponent} from '../route/dialog-info-rec-bin/dialog-info-rec-bin.component';
import {MatDialogModule} from '@angular/material/dialog';
import {GoogleChartsModule} from 'angular-google-charts';
import {MatTabsModule} from '@angular/material/tabs';
import {MatDividerModule} from '@angular/material/divider';
import {MatCardModule} from '@angular/material/card';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {ContainerService} from 'src/services/container/container.service';
import {RouteService} from 'src/services/routes/route.service';
import {AlertService} from 'src/services/login/alert/alert.service';
import {PickingService} from 'src/services/picking/picking.service';
import {HttpInterceptorInterceptor} from 'src/interceptors/httpInterceptor/http-interceptor.interceptor';
import {JwtHelperService, JWT_OPTIONS} from '@auth0/angular-jwt';
import {AdminSectionComponent} from './admin-section/admin-section.component';
import {CommonModule} from "@angular/common";
import {EmployeeRoutingModule} from "../employee/employee-routing.module";
import {MatAutocompleteModule} from "@angular/material/autocomplete";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatStepperModule} from "@angular/material/stepper";

@NgModule({
  declarations: [
    AppComponent,
    SideBarComponent,
    ToolbarComponent,
    MainPageComponent,
    HomeContentComponent,
    LoginComponent,
    DialogInfoRecBinComponent,
    AdminSectionComponent,
  ],
  imports: [
    CommonModule,
    MatAutocompleteModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatStepperModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatIconModule,
    HttpClientModule,
    FormsModule,
    MatDialogModule,
    GoogleChartsModule,
    MatTabsModule,
    MatDividerModule,
    MatCardModule,
    MatProgressBarModule,
    AgmCoreModule.forRoot({
      apiKey: '<<GOOGLE KEY>>'
    })
  ],
  providers: [
    EmployeeService,
    RecyclerBinService,
    ContainerService,
    RouteService,
    AlertService,
    PickingService,
    {provide: HTTP_INTERCEPTORS, useClass: HttpInterceptorInterceptor, multi: true},
    {provide: JWT_OPTIONS, useValue: JWT_OPTIONS}, JwtHelperService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
