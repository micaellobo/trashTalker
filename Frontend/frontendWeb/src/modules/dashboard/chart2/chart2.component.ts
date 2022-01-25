import { Component, OnInit } from '@angular/core';
import { ChartType } from 'angular-google-charts';
import { RecyclerBinService } from 'src/services/recyclerBin/recycler-bin.service';

@Component({
  selector: 'app-chart2',
  templateUrl: './chart2.component.html',
  styleUrls: ['./chart2.component.css']
})
export class Chart2Component implements OnInit {
  infoRoutes: any = []
  countPaper: number = 0;
  countGlass: number = 0;
  countPlastic: number = 0;
  countUndiff: number = 0;

  hide: string = 'none';
  alert: string = '';
  perc: number = 0;
  title = "Taxa de crescimento medio por dia";
  backgroundColor!: string;
  type!: ChartType;
  columnNames!: string[];
  public options = {
    hAxis: {
    },
    vAxis: {
      minValue: 0,
    },
    isStacked: true,
    colors: ['#6495ED', '#228B22', '#FFD700', '#FF0000'],
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
    private restRecBin: RecyclerBinService
  ) { }

  ngOnInit(): void {
    this.width = 450;
    this.height = 250;
    this.type = ChartType.BarChart;
    this.columnNames = [
      'Tipo de Residuo',
      'Papel',
      'Vidro',
      'Plástico',
      'Indiferenciado',
    ];

    this.restRecBin.showRecycleBins().subscribe((res: any[])=>{ 
      this.infoRoutes = res

      res.forEach(route => {
        route.containerDtos.forEach((container: any) => {
          if (container.typeOfWaste === 'Paper') {
            this.data[0][1] += container.avgGrowthOccupiedVolumePerDay
            this.countPaper+=1;
          }
          else if(container.typeOfWaste === 'Glass'){
            this.data[1][1] += container.avgGrowthOccupiedVolumePerDay
            this.countGlass+=1;
          }
          else if(container.typeOfWaste === 'Plastic'){
            this.data[2][1] += container.avgGrowthOccupiedVolumePerDay
            this.countPlastic+=1;
          }
          else if(container.typeOfWaste === 'Undifferentiated'){
            this.data[3][1] += container.avgGrowthOccupiedVolumePerDay
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
