import {Component, OnInit} from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {RecyclerBinService} from 'src/services/recyclerBin/recycler-bin.service';
import {RouteService} from 'src/services/routes/route.service';
import {DialogOptionRouteComponent} from '../dialog-option-route/dialog-option-route.component';
import {DialogInfoRecBinComponent} from '..//dialog-info-rec-bin/dialog-info-rec-bin.component';
import {HttpErrorResponse} from '@angular/common/http';
import {AlertService} from "../../../services/login/alert/alert.service";

@Component({
  selector: 'app-show-all-routes',
  templateUrl: './show-all-routes.component.html',
  styleUrls: ['./show-all-routes.component.css']
})
export class ShowAllRoutesComponent implements OnInit {
  title = 'google-places-autocomplete';
  lat: number = 41.39096;
  lng: number = -8.26389;
  zoom: number = 9.8;

  startFinishLat: number = 0;
  startFinishLng: number = 0;
  firstRecBin: any = 0;
  lastRecBin: any = 0;

  plannedRoutes: any[] = []
  onGoingRoutes: any[] = []
  finishedRoutes: any[] = []
  CanceledRoutes: any[] = []

  listRecBin: any[] = []
  isRouteSelected: boolean = false
  actualRouteName = ""

  currentBin: any = 0

  constructor(private restRecBin: RecyclerBinService, private restRoute: RouteService, public dialog: MatDialog, private alertService: AlertService) {
  }

  ngOnInit(): void {
    this.restRoute.getAllRoutes().subscribe((listRoutes: any[]) => {
      if (listRoutes.length > 0) {
        for (let i = 0; i < listRoutes.length; i++) {
          if (listRoutes[i].status === "Planned") {
            this.plannedRoutes.push(listRoutes[i])
          } else if (listRoutes[i].status === "Ongoing") {
            this.onGoingRoutes.push(listRoutes[i])
          } else if (listRoutes[i].status === "Finished") {
            this.finishedRoutes.push(listRoutes[i])
          } else if (listRoutes[i].status === "Canceled") {
            this.CanceledRoutes.push(listRoutes[i])
          }
        }
      }
    });
  }

  openPlanningRoute() {
    const dialogRef = this.dialog.open(DialogOptionRouteComponent, {
      width: '500px', height: 'auto'
    });
  }

  selectRecycleBin(route: any) {
    this.isRouteSelected = true;
    this.listRecBin = route.recycleBins;
    this.actualRouteName = route.name;
    this.firstRecBin = route.recycleBins[0]
    this.lastRecBin = route.recycleBins[route.recycleBins.length-1]
    this.setStartFinishPoint(route)
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
          if (typeof httpError.error === 'string' || httpError.error instanceof String){
            this.alertService.error(httpError.error, "");
          }else{
            this.alertService.error(Object.values(httpError.error).join(" and "), "");
          }
        }
      }
    );
  }

  setStartFinishPoint(route: any) {
    this.startFinishLat = route.startingPoint.latitude
    this.startFinishLng = route.startingPoint.longitude
  }

  marker(other: any): boolean {
    return true
  }
}
