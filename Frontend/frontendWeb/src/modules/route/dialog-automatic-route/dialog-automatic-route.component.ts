import {Component, OnInit} from '@angular/core';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import {Router} from '@angular/router';
import {EmployeeService} from 'src/services/employee/employee.service';
import {AlertService} from 'src/services/login/alert/alert.service';
import {RouteService} from 'src/services/routes/route.service';
import {DialogShowRouteComponent} from '../dialog-show-route/dialog-show-route.component';

@Component({
  selector: 'app-dialog-automatic-route',
  templateUrl: './dialog-automatic-route.component.html',
  styleUrls: ['./dialog-automatic-route.component.css']
})
export class DialogAutomaticRouteComponent implements OnInit {
  allEmployees: any[] = []
  route: any = {
    employeeId: "",
    dateBegin: ""
  };

  constructor(private routeService: RouteService, private employeeService: EmployeeService,
              private alertService: AlertService,
              public dialogRefe: MatDialogRef<DialogAutomaticRouteComponent>, public dialog: MatDialog,
              private router: Router) {
  }

  ngOnInit(): void {
    this.employeeService.showEmployees().subscribe((listEmployees: any[]) => {
      listEmployees.forEach((employee) => {
        if (employee.role == "Employee") {
          this.allEmployees.push(employee);
        }
      });

    }, (httpError) => {
      try {
        this.alertService.error(Object.values(httpError.error.errors).join(" and "), "");
      } catch (e) {
        if (typeof httpError.error === 'string' || httpError.error instanceof String) {
          this.alertService.error(httpError.error, "");
        } else {
          this.alertService.error(Object.values(httpError.error).join(" and "), "");
        }
      }
    });
  }

  openDialog(route: any) {
    this.dialogRefe.close();
    const dialogRef = this.dialog.open(DialogShowRouteComponent, {
      width: '600px', height: 'auto', disableClose: true
      , data: route
    });

    dialogRef.afterClosed().subscribe(result => {
      setTimeout(() => {
        location.reload()
      }, 500);
    });
  }

  submitRoute() {
    if (this.route.employeeId == "" || this.route.beginDate == "") {
      this.alertService.error("A data de inicio e o funcionário responsável são de preenchimento obrigatório.", "");
    } else {
      this.routeService.createAutomaticRoute(this.route).subscribe((route: any) => {
        this.openDialog(route);

      }, (httpError) => {
        try {
          this.alertService.error(Object.values(httpError.error.errors).join(" and "), "");
        } catch (e) {
          if (typeof httpError.error === 'string' || httpError.error instanceof String) {
            this.alertService.error(httpError.error, "");
          } else {
            this.alertService.error(Object.values(httpError.error).join(" and "), "");
          }
        }
      });
    }
  }

  goToManualSection() {
    this.dialogRefe.close();
    this.router.navigate(['home/routes/manual']);
  }
}
