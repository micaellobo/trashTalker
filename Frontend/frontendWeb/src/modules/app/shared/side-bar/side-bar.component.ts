import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {AuthService} from "../../../../services/login/auth/auth.service";

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css']
})
export class SideBarComponent implements OnInit {

  constructor(private router:Router, private readonly auth: AuthService) { }

  ngOnInit(): void {
  }

  selectEmployeesSection(){
    this.router.navigate(['home/func']);
  }

  selectHomePageSection(){
    this.router.navigate(['home']);
  }

  selectRoutesSection(){
    this.router.navigate(['home/routes']);
  }

  selectRecycleBinsSection(){
    this.router.navigate(['home/eco']);
  }

  selectAlertsSection(){
    this.router.navigate(['home/alerts']);
  }

  selectDashboardsSection(){
    this.router.navigate(['home/dashboard']);
  }

  asPermission(): boolean {
    return !this.auth.isAdmin();
  }

}
