import { Component, OnInit } from '@angular/core';
import { ChartType } from 'angular-google-charts';
import { PickingService } from 'src/services/picking/picking.service';
import { RecyclerBinService } from 'src/services/recyclerBin/recycler-bin.service';
import { RouteService } from 'src/services/routes/route.service';

@Component({
  selector: 'app-chart5',
  templateUrl: './chart5.component.html',
  styleUrls: ['./chart5.component.css']
})
export class Chart5Component implements OnInit {

  infoContainers: any = []

  countPaperJAN: number = 0;
  countGlassJAN: number = 0;
  countPlasticJAN: number = 0;
  countUndiffJAN: number = 0;

  countPaperFEV: number = 0;
  countGlassFEV: number = 0;
  countPlasticFEV: number = 0;
  countUndiffFEV: number = 0;

  countPaperMAR: number = 0;
  countGlassMAR: number = 0;
  countPlasticMAR: number = 0;
  countUndiffMAR: number = 0;

  countPaperABR: number = 0;
  countGlassABR: number = 0;
  countPlasticABR: number = 0;
  countUndiffABR: number = 0;

  countPaperMAI: number = 0;
  countGlassMAI: number = 0;
  countPlasticMAI: number = 0;
  countUndiffMAI: number = 0;

  countPaperJUN: number = 0;
  countGlassJUN: number = 0;
  countPlasticJUN: number = 0;
  countUndiffJUN: number = 0;

  countPaperJUL: number = 0;
  countGlassJUL: number = 0;
  countPlasticJUL: number = 0;
  countUndiffJUL: number = 0;

  countPaperAGO: number = 0;
  countGlassAGO: number = 0;
  countPlasticAGO: number = 0;
  countUndiffAGO: number = 0;

  countPaperSET: number = 0;
  countGlassSET: number = 0;
  countPlasticSET: number = 0;
  countUndiffSET: number = 0;

  countPaperOUT: number = 0;
  countGlassOUT: number = 0;
  countPlasticOUT: number = 0;
  countUndiffOUT: number = 0;

  countPaperNOV: number = 0;
  countGlassNOV: number = 0;
  countPlasticNOV: number = 0;
  countUndiffNOV: number = 0;

  countPaperDEZ: number = 0;
  countGlassDEZ: number = 0;
  countPlasticDEZ: number = 0;
  countUndiffDEZ: number = 0;


  backgroundColor!: string

  title = 'Media de Recolha de Residuos por Mês';
  type!: ChartType;
  data = [
    ["Jan", 0, 0, 0, 0],
    ["Feb", 0, 0, 0, 0],
    ["Mar", 0, 0, 0, 0],
    ["Apr", 0, 0, 0, 0],
    ["May", 0, 0, 0, 0],
    ["Jun", 0, 0, 0, 0],
    ["Jul", 0, 0, 0, 0],
    ["Aug", 0, 0, 0, 0],
    ["Sep", 0, 0, 0, 0],
    ["Oct", 0, 0, 0, 0],
    ["Nov", 0, 0, 0, 0],
    ["Dec", 0, 0, 0, 0]
  ];
  columnNames = ["Mes", "Papel", "Plastico", "Vidro", "Indiferenciado"];
  options = {
    hAxis: {
      title: 'Mês'
    },
    vAxis: {
      title: 'Volume'
    },
    backgroundColor: { fill: 'transparent' },

  };
  width = 900;
  height = 350;


  constructor(
    private restPicking: PickingService
  ) { }

  ngOnInit(): void {
    this.type = ChartType.LineChart;


    this.restPicking.showPickings().subscribe((listPickings: any[]) => {
      if (listPickings.length > 0) {
        for (let i = 0; i < listPickings.length; i++) {
          var date = new Date(listPickings[i].date);
          var month = date.getMonth();
          //JANEIRO
          if (month === 0) {
            if (listPickings[i].container.typeOfWaste === 'Paper') {
              this.data[0][1] += listPickings[i].volumeRecolhido;
              this.countPaperJAN += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Plastic') {
              this.data[0][2] += listPickings[i].volumeRecolhido;
              this.countPlasticJAN += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Glass') {
              this.data[0][3] += listPickings[i].volumeRecolhido;
              this.countGlassJAN += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Undifferentiated') {
              this.data[0][4] += listPickings[i].volumeRecolhido;
              this.countUndiffJAN += 1;
            }
          }
          //FEVEREIRO
          if (month === 1) {
            if (listPickings[i].container.typeOfWaste === 'Paper') {
              this.data[1][1] += listPickings[i].volumeRecolhido;
              this.countPaperFEV += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Plastic') {
              this.data[1][2] += listPickings[i].volumeRecolhido;
              this.countPlasticFEV += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Glass') {
              this.data[1][3] += listPickings[i].volumeRecolhido;
              this.countGlassFEV += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Undifferentiated') {
              this.data[1][4] += listPickings[i].volumeRecolhido;
              this.countUndiffFEV += 1;
            }
          }
          //MARÇO
          if (month === 2) {
            if (listPickings[i].container.typeOfWaste === 'Paper') {
              this.data[2][1] += listPickings[i].volumeRecolhido;
              this.countPaperMAR += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Plastic') {
              this.data[2][2] += listPickings[i].volumeRecolhido;
              this.countPlasticMAR += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Glass') {
              this.data[2][3] += listPickings[i].volumeRecolhido;
              this.countGlassMAR += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Undifferentiated') {
              this.data[2][4] += listPickings[i].volumeRecolhido;
              this.countUndiffMAR += 1;
            }
          }
          //ABRIL
          if (month === 3) {
            if (listPickings[i].container.typeOfWaste === 'Paper') {
              this.data[3][1] += listPickings[i].volumeRecolhido;
              this.countPaperABR += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Plastic') {
              this.data[3][2] += listPickings[i].volumeRecolhido;
              this.countPlasticABR += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Glass') {
              this.data[3][3] += listPickings[i].volumeRecolhido;
              this.countGlassABR += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Undifferentiated') {
              this.data[3][4] += listPickings[i].volumeRecolhido;
              this.countUndiffABR += 1;
            }
          }
          //MAIO
          if (month === 4) {
            if (listPickings[i].container.typeOfWaste === 'Paper') {
              this.data[4][1] += listPickings[i].volumeRecolhido;
              this.countPaperMAI += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Plastic') {
              this.data[4][2] += listPickings[i].volumeRecolhido;
              this.countPlasticMAI += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Glass') {
              this.data[4][3] += listPickings[i].volumeRecolhido;
              this.countGlassMAI += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Undifferentiated') {
              this.data[4][4] += listPickings[i].volumeRecolhido;
              this.countUndiffMAI += 1;
            }
          }
          //JUNHO
          if (month === 5) {
            if (listPickings[i].container.typeOfWaste === 'Paper') {
              this.data[5][1] += listPickings[i].volumeRecolhido;
              this.countPaperJUN += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Plastic') {
              this.data[5][2] += listPickings[i].volumeRecolhido;
              this.countPlasticJUN += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Glass') {
              this.data[5][3] += listPickings[i].volumeRecolhido;
              this.countGlassJUN += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Undifferentiated') {
              this.data[5][4] += listPickings[i].volumeRecolhido;
              this.countUndiffJUN += 1;
            }
          }
          //JULHO
          if (month === 6) {
            if (listPickings[i].container.typeOfWaste === 'Paper') {
              this.data[6][1] += listPickings[i].volumeRecolhido;
              this.countPaperJUL += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Plastic') {
              this.data[6][2] += listPickings[i].volumeRecolhido;
              this.countPlasticJUL += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Glass') {
              this.data[6][3] += listPickings[i].volumeRecolhido;
              this.countGlassJUL += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Undifferentiated') {
              this.data[6][4] += listPickings[i].volumeRecolhido;
              this.countUndiffJUL += 1;
            }
          }
          //AGOSTO
          if (month === 7) {
            if (listPickings[i].container.typeOfWaste === 'Paper') {
              this.data[7][1] += listPickings[i].volumeRecolhido;
              this.countPaperAGO += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Plastic') {
              this.data[7][2] += listPickings[i].volumeRecolhido;
              this.countPlasticAGO += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Glass') {
              this.data[7][3] += listPickings[i].volumeRecolhido;
              this.countGlassAGO += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Undifferentiated') {
              this.data[7][4] += listPickings[i].volumeRecolhido;
              this.countUndiffAGO += 1;
            }
          }
          //SETEMBRO
          if (month === 8) {
            if (listPickings[i].container.typeOfWaste === 'Paper') {
              this.data[8][1] += listPickings[i].volumeRecolhido;
              this.countPaperSET += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Plastic') {
              this.data[8][2] += listPickings[i].volumeRecolhido;
              this.countPlasticSET += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Glass') {
              this.data[8][3] += listPickings[i].volumeRecolhido;
              this.countGlassSET += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Undifferentiated') {
              this.data[8][4] += listPickings[i].volumeRecolhido;
              this.countUndiffSET += 1;
            }
          }
          //OUTUBRO
          if (month === 9) {
            if (listPickings[i].container.typeOfWaste === 'Paper') {
              this.data[9][1] += listPickings[i].volumeRecolhido;
              this.countPaperOUT += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Plastic') {
              this.data[9][2] += listPickings[i].volumeRecolhido;
              this.countPlasticOUT += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Glass') {
              this.data[9][3] += listPickings[i].volumeRecolhido;
              this.countGlassOUT += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Undifferentiated') {
              this.data[9][4] += listPickings[i].volumeRecolhido;
              this.countUndiffOUT += 1;
            }
          }
          //NOVEMBRO
          if (month === 10) {
            if (listPickings[i].container.typeOfWaste === 'Paper') {
              this.data[10][1] += listPickings[i].volumeRecolhido;
              this.countPaperNOV += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Plastic') {
              this.data[10][2] += listPickings[i].volumeRecolhido;
              this.countPlasticNOV += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Glass') {
              this.data[10][3] += listPickings[i].volumeRecolhido;
              this.countGlassNOV += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Undifferentiated') {
              this.data[10][4] += listPickings[i].volumeRecolhido;
              this.countUndiffNOV += 1;
            }
          }
          //DEZEMBRO
          if (month === 11) {
            if (listPickings[i].container.typeOfWaste === 'Paper') {
              this.data[11][1] += listPickings[i].volumeRecolhido;
              this.countPaperDEZ += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Plastic') {
              this.data[11][2] += listPickings[i].volumeRecolhido;
              this.countPlasticDEZ += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Glass') {
              this.data[11][3] += listPickings[i].volumeRecolhido;
              this.countGlassDEZ += 1;
            } else if (listPickings[i].container.typeOfWaste === 'Undifferentiated') {
              this.data[11][4] += listPickings[i].volumeRecolhido;
              this.countUndiffDEZ += 1;
            }
          }
        }
      }
      this.data = [
        ["Jan", Number(this.data[0][1]) / this.countPaperJAN, Number(this.data[0][2]) / this.countPlasticJAN, Number(this.data[0][3]) / this.countGlassJAN, Number(this.data[0][4]) / this.countUndiffJAN],
        ["Feb", Number(this.data[1][1]) / this.countPaperFEV, Number(this.data[1][2]) / this.countPlasticFEV, Number(this.data[1][3]) / this.countGlassFEV, Number(this.data[1][4]) / this.countUndiffFEV],
        ["Mar", Number(this.data[2][1]) / this.countPaperMAR, Number(this.data[2][2]) / this.countPlasticMAR, Number(this.data[2][3]) / this.countGlassMAR, Number(this.data[2][4]) / this.countUndiffMAR],
        ["Apr", Number(this.data[3][1]) / this.countPaperABR, Number(this.data[3][2]) / this.countPlasticABR, Number(this.data[3][3]) / this.countGlassABR, Number(this.data[3][4]) / this.countUndiffABR],
        ["May", Number(this.data[4][1]) / this.countPaperMAI, Number(this.data[4][2]) / this.countPlasticMAI, Number(this.data[4][3]) / this.countGlassMAI, Number(this.data[4][4]) / this.countUndiffMAI],
        ["Jun", Number(this.data[5][1]) / this.countPaperJUN, Number(this.data[5][2]) / this.countPlasticJUN, Number(this.data[5][3]) / this.countGlassJUN, Number(this.data[5][4]) / this.countUndiffJUN],
        ["Jul", Number(this.data[6][1]) / this.countPaperJUL, Number(this.data[6][2]) / this.countPlasticJUL, Number(this.data[6][3]) / this.countGlassJUL, Number(this.data[6][4]) / this.countUndiffJUL],
        ["Aug", Number(this.data[7][1]) / this.countPaperAGO, Number(this.data[7][2]) / this.countPlasticAGO, Number(this.data[7][3]) / this.countGlassAGO, Number(this.data[7][4]) / this.countUndiffAGO],
        ["Sep", Number(this.data[8][1]) / this.countPaperSET, Number(this.data[8][2]) / this.countPlasticSET, Number(this.data[8][3]) / this.countGlassSET, Number(this.data[8][4]) / this.countUndiffSET],
        ["Oct", Number(this.data[9][1]) / this.countPaperOUT, Number(this.data[9][2]) / this.countPlasticOUT, Number(this.data[9][3]) / this.countGlassOUT, Number(this.data[9][4]) / this.countUndiffOUT],
        ["Nov", Number(this.data[10][1]) / this.countPaperNOV, Number(this.data[10][2]) / this.countPlasticNOV, Number(this.data[10][3]) / this.countGlassNOV, Number(this.data[10][4]) / this.countUndiffNOV],
        ["Dec", Number(this.data[11][1]) / this.countPaperDEZ, Number(this.data[11][2]) / this.countPlasticDEZ, Number(this.data[11][3]) / this.countGlassDEZ, Number(this.data[11][4]) / this.countUndiffDEZ]
      ];
    });
  }
}
