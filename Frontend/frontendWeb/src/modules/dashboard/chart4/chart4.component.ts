import { Component, OnInit } from '@angular/core';
import { ChartType } from 'angular-google-charts';
import { PickingService } from 'src/services/picking/picking.service';

@Component({
  selector: 'app-chart4',
  templateUrl: './chart4.component.html',
  styleUrls: ['./chart4.component.css']
})
export class Chart4Component implements OnInit {
  infoContainers: any = []
  backgroundColor!: string

title = 'Volume por tipo de residuo Recolhido';
type!: ChartType;
  data = [
   ['Papel', 0],
   ['Plastico', 0],
   ['Vidro', 0],
   ['indiferenciado', 0] 
];
columnNames = ['Browser', 'Percentage'];
options = {    
  colors: ['#6495ED', '#228B22', '#FFD700', '#FF8066'], is3D: true,
  backgroundColor: {fill:'transparent'},
};
width!: number;
height!: number;

  constructor(
    private restPicking: PickingService
  ) { }

  ngOnInit(): void {
    this.width = 450;
    this.height = 250;
    this.type = ChartType.PieChart;

    this.restPicking.showPickings().subscribe((res: any[])=>{
      this.infoContainers = res
     
        for(let i = 0; i < this.infoContainers.length; i++){
            if(this.infoContainers[i].container.typeOfWaste === 'Paper'){
              this.data[0][1] += this.infoContainers[i].volumeRecolhido
            }
            else if(this.infoContainers[i].container.typeOfWaste === 'Glass'){
              this.data[1][1] += this.infoContainers[i].volumeRecolhido
            }
            else if(this.infoContainers[i].container.typeOfWaste === 'Plastic'){
              this.data[2][1] += this.infoContainers[i].volumeRecolhido
            }
            else if(this.infoContainers[i].container.typeOfWaste === 'Undifferentiated'){
              this.data[3][1] += this.infoContainers[i].volumeRecolhido
            }         
        }
        this.data = [
          ['Papel', this.data[0][1]],
          ['Vidro', this.data[1][1]],
          ['Plastico', this.data[2][1]],
          ['Indiferenciado', this.data[3][1]],
        ]; 
    });

  }
}



