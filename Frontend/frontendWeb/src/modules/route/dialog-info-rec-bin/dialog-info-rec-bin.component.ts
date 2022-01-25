import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ChartType } from 'angular-google-charts';

@Component({
  selector: 'app-dialog-info-rec-bin',
  templateUrl: './dialog-info-rec-bin.component.html',
  styleUrls: ['./dialog-info-rec-bin.component.css'],
})
export class DialogInfoRecBinComponent implements OnInit {
  hide: string = 'none';
  alert: string = '';
  perc: number = 0;
  title!: string;
  type!: ChartType;
  columnNames!: string[];
  public options = {
    hAxis: {
      title: 'Percentagem Ocupação',
    },
    vAxis: {
      minValue: 0,
    },
    isStacked: true,
    colors: ['#6495ED', '#228B22', '#FFD700', '#778899'],
  };
  width!: number;
  height!: number;
  data: any = [
    ['Papel', 0, 0, 0, 0],
    ['Vidro', 0, 0, 0, 0],
    ['Plástico', 0, 0, 0, 0],
    ['Indefirenciado', 0, 0, 0, 0],
  ];

  constructor(
    public dialogRef: MatDialogRef<DialogInfoRecBinComponent>,
    @Inject(MAT_DIALOG_DATA) public infoContainers: any
  ) {}

  ngOnInit(): void {
    // (this.title = 'Contentores Instalados no Ecoponto'),
      (this.type = ChartType.BarChart);
    this.columnNames = [
      'Tipo de Residuo',
      'Papel',
      'Vidro',
      'Plástico',
      'Indiferenciado',
    ];


    this.infoContainers.forEach((container: any) => {
      if (container.typeOfWaste === "Paper") {
        this.data[0][1] = container.percentageOccupied;
      }
      if (container.typeOfWaste === 'Glass') {
        this.data[1][2] = container.percentageOccupied;
      }
      if (container.typeOfWaste === 'Plastic') {
        this.data[2][3] = container.percentageOccupied;
      }
      if (container.typeOfWaste === 'Undifferentiated') {
        this.data[3][4] = container.percentageOccupied;
      }
    });
  
      this.width = 600;
      this.height = 300;
  }

  cancel(): void {
    this.hide = 'none';
    this.dialogRef.close();
  }
}
