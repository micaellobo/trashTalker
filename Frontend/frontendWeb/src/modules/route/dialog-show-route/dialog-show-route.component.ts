import {Component, Inject, OnInit} from '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import {RouteService} from "../../../services/routes/route.service";
import {publish} from "rxjs";
import {AlertService} from "../../../services/login/alert/alert.service";

@Component({
  selector: 'app-dialog-show-route',
  templateUrl: './dialog-show-route.component.html',
  styleUrls: ['./dialog-show-route.component.css']
})
export class DialogShowRouteComponent implements OnInit {
  lat: number = 41.39096;
  lng: number = -8.26389;
  zoom: number = 9.8;
  listRecBin: any[] = [];
  routeGenerated: any = 0;
  currentBin: any = 0;
  isRouteSelected: boolean = true;
  startFinishLat: number = 0;
  startFinishLng: number = 0;
  firstRecBin: any = 0;
  lastRecBin: any = 0;

  constructor(private alertService: AlertService, private routeService: RouteService, public dialogRef: MatDialogRef<DialogShowRouteComponent>
    , @Inject(MAT_DIALOG_DATA,) public generatedRoute: any) {
    this.listRecBin = generatedRoute.recycleBins;
    this.routeGenerated = generatedRoute;
  }

  ngOnInit(): void {
    this.setStartFinishPoint(this.routeGenerated)
    this.firstRecBin = this.routeGenerated.recycleBins[0]
    this.lastRecBin = this.routeGenerated.recycleBins[this.routeGenerated.recycleBins.length-1]
  }

  confirm() {
    this.routeService.confirmeRoute(this.routeGenerated.id).subscribe({
      next: (data) => {
        this.alertService.operationsSucced("Rota criado com sucesso!")
        setTimeout(()=>{
          this.dialogRef.close();
        },1000);
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
        setTimeout(()=>{
         location.reload()
        },500);
      },
    });
  }

  cancelRoute() {
    this.routeService.cancelRoute(this.routeGenerated.id).subscribe({
      next: (data) => {
        this.dialogRef.close();
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

  openInfo(id: String) {
  }

  onMouseOver(index: any) {
    this.listRecBin[this.currentBin].display = false;
    this.listRecBin[index].display = true;
    this.currentBin = index;
  }

  setStartFinishPoint(route: any) {
    this.startFinishLat = route.startingPoint.latitude
    this.startFinishLng = route.startingPoint.longitude
  }

  marker(other: any): boolean {
    return true
  }

}
