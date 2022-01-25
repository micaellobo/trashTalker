import {Component, Input, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {employee} from 'src/models/employee';
import {EmployeeService} from 'src/services/employee/employee.service';
import {UploadFileService} from 'src/services/files/upload-file.service';
import {AlertService} from 'src/services/login/alert/alert.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {
  fileUpload: File = new File([], "");
  fileName = '';
  message = '';

  @Input() emp: employee = new employee("", "", "", "EMPLOYEE", "", "", "", "", "active", "", "", "", "");

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
      next: async (data) => {
        this.uploadFile(data.id)
        this.alertService.operationsSucced("Funcionário registado com sucesso!");
        this.emp = new employee("", "", "", "EMPLOYEE", "", "", "", "", "active", "", "", "", "");
        this.router.navigate(["home/func"])
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
