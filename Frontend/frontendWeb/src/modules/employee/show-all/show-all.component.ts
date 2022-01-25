import {Component, OnInit} from '@angular/core';
import {FormControl} from '@angular/forms';
import {MatDialog} from '@angular/material/dialog';
import {ActivatedRoute, Router} from '@angular/router';
import {employee} from 'src/models/employee';
import {EmployeeService} from 'src/services/employee/employee.service';
import {AlertService} from 'src/services/login/alert/alert.service';
import {DisableComponent} from '../disable/disable.component';
import {UpdateComponent} from '../update/update.component';
import {AuthGuardService} from "../../../services/login/auth-guard/auth-guard.service";

@Component({
  selector: 'app-show-all',
  templateUrl: './show-all.component.html',
  styleUrls: ['./show-all.component.css']
})
export class ShowAllComponent implements OnInit {
  employees: any[] = [];
  myControl = new FormControl();
  options: any[] = [];
  selectedEmployee: employee = new employee("", "", "", "", "", "", "", "", "", "", "", "", "");

  constructor(private router: Router, private route: ActivatedRoute, private restEmployees: EmployeeService,
              public dialog: MatDialog, private alertService: AlertService, private readonly auth: AuthGuardService) {
  }

  ngOnInit(): void {
    this.restEmployees.showEmployees().subscribe((listEmployees: any[]) => {
      if (listEmployees.length > 0) {
        this.employees = listEmployees;
        this.employees = this.employees.filter(emp => emp.role == "Employee");
        let em = this.employees[0];
        this.selectedEmployee = new employee(em.id, em.username, em.password, em.role, em.firstName
          , em.lastName, em.email, em.gender, em.status, em.street, em.city, em.zipCode, em.country);
      }
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

  selectFunc(em: any) {
    this.selectedEmployee = new employee(em.id, em.username, em.password, em.role, em.firstName
      , em.lastName, em.email, em.gender, em.status, em.street, em.city, em.zipCode, em.country);
  }

  disableEmployee() {
    if (this.employees.length != 0 && this.selectedEmployee._id != "") {
      const dialogRef = this.dialog.open(DisableComponent, {
        width: '500px', height: 'auto'
        , data: this.selectedEmployee
      });

      dialogRef.afterClosed().subscribe(result => {
        if (localStorage.getItem("disable") === 'ok') {
          this.restEmployees.disableEmployee(this.selectedEmployee._id).subscribe((res) => {
            this.alertService.operationsSucced("Funcionário desativado com sucesso!");
            this.selectedEmployee.status = "INACTIVE";
            localStorage.removeItem("disable");
            setTimeout(() => location.reload(), 300);
          }, ((error) => {
            localStorage.removeItem("disable");
            this.alertService.operationsSucced("Funcionário desativado com sucesso!");
          }));
        } else if (localStorage.getItem("disable") === 'cancel') {
          localStorage.removeItem("disable");
        } else {
          localStorage.removeItem("disable");
        }
      });
    } else {
      this.alertService.info("Nenhum funcionário foi selecionado!", "Operação Inválida");
    }
  }

  addEmployee() {
    this.router.navigate(['home/func/add']);
  }

  updateEmployee() {
    if (this.employees.length != 0 && this.selectedEmployee._id != "") {
      const dialogRef = this.dialog.open(UpdateComponent, {
        width: '600px', height: 'auto'
        , data: this.selectedEmployee
      });

      dialogRef.afterClosed().subscribe(result => {
        if (localStorage.getItem("updated") === 'ok') {
          let res = JSON.parse(localStorage.getItem("updatedEmployee")!);
          this.restEmployees.updateEmployee(this.selectedEmployee._id, res).subscribe((res) => {
            this.alertService.operationsSucced("Funcionário atualizado com sucesso!");
            localStorage.removeItem("updated");
            localStorage.removeItem("updatedEmployee");
            setTimeout(() => location.reload(), 300);
          }, ((httpError) => {
            localStorage.removeItem("updated");
            localStorage.removeItem("updatedEmployee");
            try {
              this.alertService.error(Object.values(httpError.error.errors).join(" and "), "");
            } catch (e) {
              if (typeof httpError.error === 'string' || httpError.error instanceof String) {
                this.alertService.error(httpError.error, "");
              } else {
                this.alertService.error(Object.values(httpError.error).join(" and "), "");
              }
            }
          }));
        } else if (localStorage.getItem("updated") === 'cancel') {
          localStorage.removeItem("updated");
        } else {
          localStorage.removeItem("updated");
        }
      });
    } else {
      this.alertService.info("Nenhum funcionário foi selecionado!", "Operação Inválida");
    }
  }
}
