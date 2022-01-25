import {Component, Input, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import { EmployeeService } from 'src/services/employee/employee.service';
import {RecyclerBinService} from 'src/services/recyclerBin/recycler-bin.service';
import {RouteService} from 'src/services/routes/route.service';
import {AlertService} from "../../../services/login/alert/alert.service";

@Component({
  selector: 'app-manual-route',
  templateUrl: './manual-route.component.html',
  styleUrls: ['./manual-route.component.css'],
})
export class ManualRouteComponent implements OnInit {
  @Input() newRoute = {name: '', dateBegin: '', recycleBinIds: [], employeeId: ''};
  allEmployees:any[] = []

  constructor(
    private router: Router,
    private restRecBin: RecyclerBinService,
    private restRoute: RouteService,
    private alertService: AlertService,
    private employeeService: EmployeeService
  ) {
  }

  recBinsList: any[] = [];
  recBinsAvailable: any[] = [];
  recBinsRoute: any[] = [];
  currentRecBin: any = {};
  selectedRecBinRoute: any = null;
  prevision: any[] = [];
  previsionDays: any[] = [];

  ngOnInit(): void {
    this.restRecBin.showRecycleBins().subscribe((listRec: any[]) => {
      if (listRec.length > 0) {
        listRec.forEach(element => {
          if (element.status == "Active") {
            this.recBinsList.push(element)
          }
        });
        this.recBinsAvailable = this.recBinsList;
        this.currentRecBin = this.recBinsList[0];
        this.calculatePrevisionFromRecBin(this.currentRecBin);
      }
    });
    this.employeeService.showEmployees().subscribe((listEmployees:any[])=>{
      listEmployees.forEach((employee)=>{
          if(employee.role == "Employee"){
            this.allEmployees.push(employee);
          }
      });
  },(httpError) => {
      try {
        this.alertService.error(Object.values(httpError.error.errors).join(" and "), "");
      } catch (e) {
        if (typeof httpError.error === 'string' || httpError.error instanceof String){
          this.alertService.error(httpError.error, "");
        }else{
          this.alertService.error(Object.values(httpError.error).join(" and "), "");
        }
      }
  });
  }

  extractIds(list: any[]): any {
    let listIds = [];
    for (let i = 0; i < this.recBinsRoute.length; i++) {
      let currentId = this.recBinsRoute[i].id;
      listIds.push(currentId);
    }
    return {
      name: this.newRoute.name,
      dateBegin: this.newRoute.dateBegin,
      recycleBinIds: listIds,
      employeeId: this.newRoute.employeeId == '' ? 'dc06815f-bac7-42bf-b979-5cc768b745ac' : this.newRoute.employeeId
    };
  }

  submitRoute() {
    if (this.newRoute.dateBegin == '') {
      this.alertService.error("The field \"Date Inicio\" is required", "");
      return
    }

    this.restRoute.createManualRoute(this.extractIds(this.recBinsRoute)).subscribe((res: any) => {
        this.alertService.operationsSucced('Rota adicionada com sucesso');
        setTimeout(() => {
          this.router.navigate(['home/routes']);
        }, 1500);
      },
      (httpError) => {
        try {
          this.alertService.error(Object.values(httpError.error.errors).join(" and "), "");
        } catch (e) {
          if (typeof httpError.error === 'string' || httpError.error instanceof String){
            this.alertService.error(httpError.error, "");
          }else{
            this.alertService.error(Object.values(httpError.error).join(" and "), "");
          }
        }
      }
    );
  }


  moveUp() {
    if (this.selectedRecBinRoute != null && this.selectedRecBinRoute != 0) {
      let temp = this.recBinsRoute[this.selectedRecBinRoute];
      this.recBinsRoute[this.selectedRecBinRoute] =
        this.recBinsRoute[this.selectedRecBinRoute - 1];
      this.recBinsRoute[this.selectedRecBinRoute - 1] = temp;
      this.selectedRecBinRoute--;
    }
  }

  moveDown() {
    if (
      this.selectedRecBinRoute != null &&
      this.selectedRecBinRoute + 1 != this.recBinsRoute.length
    ) {
      let temp = this.recBinsRoute[this.selectedRecBinRoute];
      this.recBinsRoute[this.selectedRecBinRoute] =
        this.recBinsRoute[this.selectedRecBinRoute + 1];
      this.recBinsRoute[this.selectedRecBinRoute + 1] = temp;
      this.selectedRecBinRoute++;
    }
  }

  selectRecBinfromAvailable(bin: any) {
    this.currentRecBin = bin;
    this.calculatePrevisionFromRecBin(bin);
    this.selectedRecBinRoute = null;
    this.recBinsRoute.push(bin);
    for (let i = 0; i < this.recBinsAvailable.length; i++) {
      if (this.recBinsAvailable[i].id === bin.id) {
        this.recBinsAvailable.splice(i, 1);
      }
    }
  }

  selectRecBinfromRoute(bin: any, i: number) {
    this.selectedRecBinRoute = i;
    this.currentRecBin = bin;
    this.calculatePrevisionFromRecBin(bin);
  }

  removeFromRoute() {
    if (this.selectedRecBinRoute != null) {
      this.recBinsAvailable.push(this.recBinsRoute[this.selectedRecBinRoute]);
      this.recBinsRoute.splice(this.selectedRecBinRoute, 1);
    }
    this.selectedRecBinRoute = null;
  }

  calculatePrevisionDate(container: any) {
    var daysToBeFull = (100-container.percentageOccupied)/container.avgGrowthOccupiedVolumePerDay;

    var today = new Date();

    // 1 dia = 86400 segundos
    const seconds = daysToBeFull * 86400

    var actualDate = new Date(today);

    // Se seconds == 0, significa que o contentor já está cheio
    if (seconds != 0) {
      today.setSeconds(seconds);
    }

    var previsionDate = today;

    if (previsionDate.getTime() === actualDate.getTime()) {
      return "O contentor já se encontra cheio à: "
    }

    return previsionDate.toLocaleDateString()
  }

  changeDateFormat(timeSpan: string) {
    var dateArray = timeSpan.split(".")
    var newDate = ""

    if (dateArray.length >= 3) {
      newDate += dateArray[0] + " d "
      var s = dateArray[1].split(":")
    } else {
      var s = dateArray[0].split(":")
    }
    newDate += s[0] + " h "
    newDate += s[1] + " m "
    newDate += s[2] + " s "

    if(newDate.includes("-")) {
      var result = newDate.split("-")
      return result[1]
    }

    return newDate
  }

  calculatePrevisionFromRecBin(recBin: any) {
    this.prevision[0] = this.calculatePrevisionDate(recBin.containerDtos[0])
    this.previsionDays[0] = this.changeDateFormat(recBin.containerDtos[0].previsionOrDaysFull)
    this.prevision[1] = this.calculatePrevisionDate(recBin.containerDtos[1])
    this.previsionDays[1] = this.changeDateFormat(recBin.containerDtos[1].previsionOrDaysFull)
    this.prevision[2] = this.calculatePrevisionDate(recBin.containerDtos[2])
    this.previsionDays[2] = this.changeDateFormat(recBin.containerDtos[2].previsionOrDaysFull)
    this.prevision[3] = this.calculatePrevisionDate(recBin.containerDtos[3])
    this.previsionDays[3] = this.changeDateFormat(recBin.containerDtos[3].previsionOrDaysFull)
  }


}
