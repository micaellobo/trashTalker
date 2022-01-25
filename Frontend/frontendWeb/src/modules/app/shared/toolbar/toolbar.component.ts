import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/services/login/auth/auth.service';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.css']
})
export class ToolbarComponent implements OnInit {
  username: any;

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.username = this.authService.getCurrentUser().username;
  }

  logout(): void {
    this.authService.clear();
    this.router.navigate([""])
  }

}
