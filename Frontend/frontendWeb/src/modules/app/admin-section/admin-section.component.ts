import {Component, Input, OnInit} from '@angular/core';
import {employee} from "../../../models/employee";
import {Router} from "@angular/router";
import {UploadFileService} from "../../../services/files/upload-file.service";
import {EmployeeService} from "../../../services/employee/employee.service";
import {AlertService} from "../../../services/login/alert/alert.service";

@Component({
  selector: 'app-admin-section',
  templateUrl: './admin-section.component.html',
  styleUrls: ['./admin-section.component.css']
})
export class AdminSectionComponent implements OnInit {
  fileUpload: File = new File([], "");
  fileName = '';
  message = '';

  @Input() emp: employee = new employee("", "", "", "", "", "", "", "", "active", "", "", "", "");

  constructor(private router: Router, private restUploadFile: UploadFileService,
              private restEmployees: EmployeeService, private alertService: AlertService) {
  }

  ngOnInit(): void {
  }

  cancelOperation() {
    this.router.navigate(['home/func']);
  }

  addEmployee() {
    if (this.fileUpload.size == 0) {
      this.alertService.error("Necessario imagem", "");
      return
    }
    this.restEmployees.addEmployee(this.emp).subscribe({
      next: (data) => {
        this.uploadFile(data.id)
        this.alertService.operationsSucced("Funcionário registado com sucesso!");
        this.emp = new employee("", "", "", "", "", "", "", "", "active", "", "", "", "");
      },
      error: (httpError) => {
        try {
          this.alertService.error(Object.values(httpError.error.errors).join(" and "), "");
        } catch (e) {
          if (typeof httpError.error === 'string' || httpError.error instanceof String) {
            this.alertService.error(httpError.error, "");
          } else {
            this.alertService.error(Object.values(httpError.error).join(" and "), "");
          }
        }
      },
    });
  }

  uploadFile(id: any) {
    this.restUploadFile.uploadFile(this.fileUpload, id).subscribe((result: any) => {
      this.fileName = '';
      this.message = '';
    }, (httpError) => {
      try {
        console.log(httpError)
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

  onFileSelected(event: any) {
    this.fileUpload = event.target.files[0];
    this.fileName = this.fileUpload.name;
  }

}
