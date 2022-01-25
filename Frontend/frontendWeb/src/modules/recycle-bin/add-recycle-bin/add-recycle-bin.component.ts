import {HttpErrorResponse} from '@angular/common/http';
import {Component, Input, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {recycleBin} from 'src/models/recycleBin';
import {AlertService} from 'src/services/login/alert/alert.service';
import {RecyclerBinService} from 'src/services/recyclerBin/recycler-bin.service';

@Component({
  selector: 'app-add-recycle-bin',
  templateUrl: './add-recycle-bin.component.html',
  styleUrls: ['./add-recycle-bin.component.css']
})
export class AddRecycleBinComponent implements OnInit {
  @Input() rb: recycleBin = new recycleBin("", "", 0, 0, "", "", "", "", []);

  constructor(private router: Router, private restRecycleBin: RecyclerBinService,
              private alertService: AlertService) {
  }

  ngOnInit(): void {
  }

  cancelOperation() {
    this.router.navigate(['home/eco']);
  }

  addRecycleBin() {
    this.restRecycleBin.addRecycleBin(this.rb).subscribe((res) => {
        this.rb = new recycleBin("", "", 0, 0, "", "", "", "", []);
        this.alertService.operationsSucced("Novo ecoponto adicionado com sucesso!");
        this.router.navigate(['home/eco']);
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
      }
    );


  }


}
