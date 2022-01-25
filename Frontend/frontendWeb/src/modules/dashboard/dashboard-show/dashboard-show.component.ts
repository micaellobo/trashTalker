import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { AlertService } from 'src/services/login/alert/alert.service';
import { PickingService } from 'src/services/picking/picking.service';
import { RecyclerBinService } from 'src/services/recyclerBin/recycler-bin.service';
import { RouteService } from 'src/services/routes/route.service';

@Component({
  selector: 'app-dashboard-show',
  templateUrl: './dashboard-show.component.html',
  styleUrls: ['./dashboard-show.component.css']
})
export class DashboardShowComponent implements OnInit {
  totalRotas: number = 0;
  totalPlaneadas: number = 0;
  totalDecorrer: number = 0;
  totalCanceladas: number = 0;
  totalTerminadas: number = 0;
  volumeResiduosRecolhidos: number = 0;
  numeroEcopontosLimite: number = 0;
  totalPaperVolume: number = 0;
  totalGlassVolume: number = 0;
  totalPlasticVolume: number = 0;
  totalUndifferentiatedVolume: number = 0;
  nameRoute: any[] = [];
  isdate = false;


  @Input() dateBegin = '';
  @Input() dateEnd = '';



  constructor(
    public dialog: MatDialog,
    private restRoute: RouteService,
    private restPicking: PickingService,
    private restRecycleBin: RecyclerBinService,
    private alertService:AlertService
  ) { }

  ngOnInit(): void {

    if (this.dateBegin === '' || this.dateEnd === '' ) {
      this.isdate = true;
      this.withNoDate()
    } else {
      this.withdate()
    }
  }

  withNoDate() {
    //Total rotas
    this.restRoute.getAllRoutes().subscribe((listRoutes: any[]) => {
      if (listRoutes.length > 0) {
        for (let i = 0; i < listRoutes.length; i++) {
            this.totalRotas++;
            if (listRoutes[i].status === 'Planned') {
              this.totalPlaneadas++;
            }else if(listRoutes[i].status === 'Ongoing'){
              this.totalDecorrer++;
            }else if(listRoutes[i].status === 'Finished'){
              this.totalTerminadas++;
            }else if(listRoutes[i].status === 'Canceled'){
              this.totalCanceladas++;
            }
        }
      }
    });
    //Volume residuos totais
    this.restPicking.showPickings().subscribe((listPickings: any[]) => {
      if (listPickings.length > 0) {
        for (let i = 0; i < listPickings.length; i++) {
          this.volumeResiduosRecolhidos += listPickings[i].volumeRecolhido
        }
      }
    });

    // volume residuos por tipo de residuo
    this.restPicking.showPickings().subscribe((picking: any[]) => {
      if (picking.length > 0) {
        for (let i = 0; i < picking.length; i++) {
          if (picking[i].container.typeOfWaste === 'Paper') {
            this.totalPaperVolume += picking[i].volumeRecolhido
          }
          else if (picking[i].container.typeOfWaste === 'Glass') {
            this.totalGlassVolume += picking[i].volumeRecolhido
          }
          else if (picking[i].container.typeOfWaste === 'Plastic') {
            this.totalPlasticVolume += picking[i].volumeRecolhido
          }
          else if (picking[i].container.typeOfWaste === 'Undifferentiated') {
            this.totalUndifferentiatedVolume += picking[i].volumeRecolhido
          }
        }
      }
    });

    // Numero de Ecopontos no limite da capacidade
    this.restRecycleBin.showRecycleBins().subscribe((percentage: any[]) => {
      if (percentage.length > 0) {
        for (let i = 0; i < percentage.length; i++) {
          for (let j = 0; j < 4; j++) {
            if (percentage[i].containerDtos[j].percentageOccupied > 90) {
              this.numeroEcopontosLimite += 1
              break
            }
          }
        }
      }
    });
    // Nome das Rotas
    this.restRoute.getAllRoutes().subscribe((listRoutes: any[]) => {
      if (listRoutes.length > 0) {
        for (let i = 0; i < listRoutes.length; i++) {
          this.nameRoute[i] = listRoutes[i].name;
        }
      }
    });
  }



  withdate() {
    if(this.dateBegin === "" || this.dateEnd === ""){
      alert("Por favor introduza uma data!");
      return
    }
    if(this.dateBegin > this.dateEnd){
      alert("A data de inicio não pode ser superior a data de fim!");
      return
    }

    //Total rotas
    this.totalRotas = 0;
    this.totalPlaneadas = 0;
    this.totalTerminadas = 0;
    this.totalCanceladas = 0;
    this.totalDecorrer = 0;
    this.volumeResiduosRecolhidos = 0;
    this.totalPaperVolume = 0;
    this.totalGlassVolume = 0;
    this.totalPlasticVolume = 0;
    this.totalUndifferentiatedVolume = 0;


    this.restRoute.getAllRoutes().subscribe((listRoutes: any[]) => {
      if (listRoutes.length > 0) {
        for (let i = 0; i < listRoutes.length; i++) {
          if (listRoutes[i].dateBegin >= this.dateBegin && listRoutes[i].dateBegin <= this.dateEnd) {
            if (listRoutes[i].status === 'Planned') {
              this.totalPlaneadas++;
            }else if(listRoutes[i].status === 'Ongoing'){
              this.totalDecorrer++;
            }else if(listRoutes[i].status === 'Finished'){
              this.totalTerminadas++;
            }else if(listRoutes[i].status === 'Canceled'){
              this.totalCanceladas++;
            }
          }
        }
        this.totalRotas+=this.totalPlaneadas+this.totalDecorrer+this.totalTerminadas+this.totalCanceladas;
      }
    });
    //Volume residuos totais
    this.restPicking.showPickings().subscribe((listPickings: any[]) => {
      if (listPickings.length > 0) {
        for (let i = 0; i < listPickings.length; i++) {
          if (listPickings[i].date >= this.dateBegin && listPickings[i].date <= this.dateEnd) {
            this.volumeResiduosRecolhidos += listPickings[i].volumeRecolhido
          }
        }
      }
    });
    // volume residuos por tipo de residuo
    this.restPicking.showPickings().subscribe((picking: any[]) => {
      if (picking.length > 0) {
        for (let i = 0; i < picking.length; i++) {
          if (picking[i].date >= this.dateBegin && picking[i].date <= this.dateEnd) {
            if (picking[i].container.typeOfWaste === 'Paper') {
              this.totalPaperVolume += picking[i].volumeRecolhido
            }
            else if (picking[i].container.typeOfWaste === 'Glass') {
              this.totalGlassVolume += picking[i].volumeRecolhido
            }
            else if (picking[i].container.typeOfWaste === 'Plastic') {
              this.totalPlasticVolume += picking[i].volumeRecolhido
            }
            else if (picking[i].container.typeOfWaste === 'Undifferentiated') {
              this.totalUndifferentiatedVolume += picking[i].volumeRecolhido
            }
          }
        }
      }
    });
  }
}

