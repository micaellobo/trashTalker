import { Component, OnInit } from '@angular/core';
import { ChartType } from 'angular-google-charts';
import { RouteService } from 'src/services/routes/route.service';

@Component({
  selector: 'app-chart3',
  templateUrl: './chart3.component.html',
  styleUrls: ['./chart3.component.css']
})
export class Chart3Component implements OnInit {
  count: number = 0;

  infoRoutes: any = []
  hide: string = 'none';
  alert: string = '';
  perc: number = 0;
  title = "Total de Rotas por tipo de Marcação";
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
    backgroundColor: { fill: 'transparent' },
  };
  width!: number;
  height!: number;
  data: any = [
    ["A Decorrer", 0, 0],
    ["Planeada", 0, 0],
    ["Cancelada", 0, 0],
    ["Terminada", 0, 0],
  ];


  constructor(
    private restRoute: RouteService
  ) { }

  ngOnInit(): void {
    this.width = 450;
    this.height = 250;
    this.type = ChartType.LineChart;
    this.  columnNames = ["Month", "Auto", "Manual"];

    this.restRoute.getAllRoutes().subscribe((res: any[]) => { 
      this.infoRoutes = res
      for (let i = 0; i < this.infoRoutes.length; i++) {
        if(this.infoRoutes[i].typeCreation === "Auto"){

          if (this.infoRoutes[i].status === 'Planned') {
            this.data[0][1] +=1;
          }
          else if (this.infoRoutes[i].status === 'Ongoing') {
            this.data[1][1] +=1;
          }
          else if (this.infoRoutes[i].status === 'Canceled') {
            this.data[2][1] +=1;
          }
          else if (this.infoRoutes[i].status === 'Finished') {
            this.data[3][1] +=1;
          }
        }
        
        else if(this.infoRoutes[i].typeCreation === "Manual"){
          if (this.infoRoutes[i].status === 'Planned') {
            this.data[0][2] +=1;
          }
          else if (this.infoRoutes[i].status === 'Ongoing') {
            this.data[1][2] +=1;
          }
          else if (this.infoRoutes[i].status === 'Canceled') {
            this.data[2][2] +=1;
          }
          else if (this.infoRoutes[i].status === 'Finished') {
            this.data[3][2] +=1;
          }
        }
      }

      this.data = [
        ['Planeada', this.data[0][1], this.data[0][2]],
        ['A Decorrer', this.data[1][1], this.data[1][2]],
        ['Cancelada', this.data[2][1], this.data[2][2]],
        ['Terminada', this.data[3][1], this.data[3][2]],
      ];
    });
    

  }
}