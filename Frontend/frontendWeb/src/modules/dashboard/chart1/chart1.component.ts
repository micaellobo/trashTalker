import { Component, Inject, OnInit } from '@angular/core';
import { ChartType } from 'angular-google-charts';
import { RecyclerBinService } from 'src/services/recyclerBin/recycler-bin.service';

@Component({
  selector: 'app-chart1',
  templateUrl: './chart1.component.html',
  styleUrls: ['./chart1.component.css']
})
export class Chart1Component implements OnInit {
  infoContainers: any = []

  countPaper: number = 0;
  countGlass: number = 0;
  countPlastic: number = 0;
  countUndiff: number = 0;



  hide: string = 'none';
  alert: string = '';
  perc: number = 0;
  backgroundColor!: string;
  title= "Percentagem Ocupação media Atual";
  type!: ChartType;
  columnNames!: string[];
  
  
  public options = {
    hAxis: {
    },
    vAxis: {
      minValue: 0,
    },
    seriesType: 'bars',
    series: {4: {type: 'bar'}},

    isStacked: true,
    colors: ['#6495ED', '#228B22', '#FFD700', '#778899'],
    backgroundColor: {fill:'transparent'},
  };
  width!: number;
  height!: number;
  data: any = [
    ['Papel', 0, 0, 0, 0],
    ['Vidro', 0, 0, 0, 0],
    ['Plastico', 0, 0, 0, 0],
    ['Indif', 0, 0, 0, 0],
  ];

  constructor(
    private restBins: RecyclerBinService
  ) { }

  ngOnInit() {
    this.width = 450;
    this.height = 250;
    this.type = ChartType.ComboChart;
    this.columnNames = [
      'Tipo de Residuo',
      'Papel',
      'Vidro',
      'Plástico',
      'Indiferenciado',
    ];

    this.restBins.showRecycleBins().subscribe((res: any[]) => {
      this.infoContainers = res

      res.forEach(bin => {
        bin.containerDtos.forEach((container: any) => {
          if (container.typeOfWaste === 'Paper') {
            this.data[0][1] += container.percentageOccupied
            this.countPaper+=1;
          }
          else if(container.typeOfWaste === 'Glass'){
            this.data[1][1] += container.percentageOccupied
            this.countGlass+=1;
          }
          else if(container.typeOfWaste === 'Plastic'){
            this.data[2][1] += container.percentageOccupied
            this.countPlastic+=1;
          }
          else if(container.typeOfWaste === 'Undifferentiated'){
            this.data[3][1] += container.percentageOccupied
            this.countUndiff+=1;
          }
        });
      });
      this.data = [
        ['Papel', this.data[0][1]/this.countPaper, 0, 0, 0],
        ['Vidro', 0, this.data[1][1]/this.countGlass, 0, 0],
        ['Plastico', 0, 0, this.data[2][1]/this.countPlastic, 0],
        ['Indif', 0, 0, 0, this.data[3][1]/this.countUndiff],
      ];
    });
  }
}
