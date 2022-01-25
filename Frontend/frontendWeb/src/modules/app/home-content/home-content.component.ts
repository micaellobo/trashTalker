import {HttpErrorResponse} from '@angular/common/http';
import {Component, OnInit} from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {ContainerService} from 'src/services/container/container.service';
import {RecyclerBinService} from 'src/services/recyclerBin/recycler-bin.service';
import {RouteService} from 'src/services/routes/route.service';
import {DialogInfoRecBinComponent} from '../../route/dialog-info-rec-bin/dialog-info-rec-bin.component';
import {Router} from '@angular/router';
import {AlertService} from "../../../services/login/alert/alert.service";

@Component({
  selector: 'app-home-content',
  templateUrl: './home-content.component.html',
  styleUrls: ['./home-content.component.css'],
})
export class HomeContentComponent implements OnInit {
  title = 'google-places-autocomplete';

  lat: number = 41.39096;
  lng: number = -8.26389;
  zoom: number = 9.8;
  currentBin: any = 0;
  listRecBin: any[] = [];
  containersList: any[] = [];
  containersListAlert: any[] = [];

  isRouteSelected: boolean = false;

  latlng: any[] = [];

  previsionDays: any[] = []

  constructor(
    private restRecBin: RecyclerBinService,
    private containerService: ContainerService,
    public dialog: MatDialog,
    private restRoute: RouteService,
    private router: Router,
    private alertService: AlertService
  ) {
  }

  ngOnInit(): void {
    this.restRecBin.showRecycleBins().subscribe((recBins: any) => {
      if (recBins.length > 0) {
        this.listRecBin = recBins;
        for (let i = 0; i < this.listRecBin.length; i++) {
          if (recBins[i].containerDtos.length > 0) {
            //this.listRecBin[i].display = false; (Provavelmente n faz nada)
            for (let j = 0; j < recBins[i].containerDtos.length; j++) {
              this.containersList.push(recBins[i].containerDtos[j]);
            }
          }
        }
      }
    });


    this.containerService.getContainersOnAlert().subscribe((containers: any) => {
      this.containersListAlert = containers;
    });
  }


  selectRecycleBin(em: any) {
    this.isRouteSelected = true;
    this.listRecBin = em;
  }


  onMouseOver(index: any) {
    this.listRecBin[this.currentBin].display = false;
    this.listRecBin[index].display = true;
    this.currentBin = index;
  }

  openInfo(id: String) {

    this.restRecBin.showRecycleBin(id).subscribe(
      (recBin) => {
        this.dialog.open(DialogInfoRecBinComponent, {
          width: 'auto',
          height: 'auto',
          data: recBin.containerDtos,
        });
      },
      (httpError) => {
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

  selectAlertsSection() {
    this.router.navigate(['home/alerts']);
  }

  findIndexOfRecBin(id: string) {
    for (let index = 0; index < this.listRecBin.length; index++) {
      if (this.listRecBin[index].id == id) {
        this.onMouseOver(index);
        break;
      }
    }
  }

  calculatePrevisionDate(container: any) {
    var daysToBeFull = (100 - container.percentageOccupied) / container.avgGrowthOccupiedVolumePerDay;

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

    if (newDate.includes("-")) {
      var result = newDate.split("-")
      return result[1]
    }

    return newDate
  }

  marker(other: any): boolean {
    return true
  }

}
