import {Component, OnInit} from '@angular/core';
import {FormControl} from '@angular/forms';
import {MatDialog} from '@angular/material/dialog';
import {Router, ActivatedRoute} from '@angular/router';
import {ChartType} from 'angular-google-charts';
import {recycleBin} from 'src/models/recycleBin';
import {AlertService} from 'src/services/login/alert/alert.service';
import {RecyclerBinService} from 'src/services/recyclerBin/recycler-bin.service';
import {DisableRecycleBinComponent} from '../disable-recycle-bin/disable-recycle-bin.component';
import {UpdateRecycleBinComponent} from '../update-recycle-bin/update-recycle-bin.component';

@Component({
  selector: 'app-show-all-recycle-bin',
  templateUrl: './show-all-recycle-bin.component.html',
  styleUrls: ['./show-all-recycle-bin.component.css']
})
export class ShowAllRecycleBinComponent implements OnInit {
  backgroundColor!: string;
  title!: string;
  public options = {
    hAxis: {
      title: 'Percent Occupation',
    },
    vAxis: {
      minValue: 0,
    },
    isStacked: true,
    colors: ['#6495ED', '#228B22', '#FFD700', '#778899'],
    backgroundColor: {fill: 'transparent'},
  };
  width: number = 550;
  height!: number;
  data: any = [
    ['Paper', 0, 0, 0, 0],
    ['Glass', 0, 0, 0, 0],
    ['Plastic', 0, 0, 0, 0],
    ['Undif', 0, 0, 0, 0],
  ];
  type = ChartType.BarChart;
  columnNames = [
    'Tipo de Residuo',
    'Papel',
    'Vidro',
    'Plástico',
    'Indiferenciado',
  ];

  listRecycleBins: any[] = [];
  myControl = new FormControl();
  selectedRecycleBin: recycleBin = new recycleBin("", "", 0, 0, "", "", "", "", []);
  container0Type: any;
  container1Type: any;
  container2Type: any;
  container3Type: any;
  recBin: any;

  constructor(private router: Router, private route: ActivatedRoute,
              private restRecycleBins: RecyclerBinService, private alertService: AlertService,
              public dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.restRecycleBins.showRecycleBins().subscribe((listRecycleBins: any[]) => {
      if (listRecycleBins.length > 0) {
        this.listRecycleBins = listRecycleBins;
        for (let i = 0; i < this.listRecycleBins.length; i++) {
          if (listRecycleBins[i].containerDtos.length > 0) {
            for (let j = 0; j < listRecycleBins[i].containerDtos.length; j++) {
              if (listRecycleBins[i].containerDtos[j].percentageOccupied == undefined) {
                listRecycleBins[i].containerDtos[j].percentageOccupied = 0;
              }
            }
          }
        }
        let rb = listRecycleBins[0];
        this.selectRB(rb)

        this.container0Type = this.getTypeOfWaist(rb.containerDtos[0].typeOfWaste);
        this.container1Type = this.getTypeOfWaist(rb.containerDtos[1].typeOfWaste);
        this.container2Type = this.getTypeOfWaist(rb.containerDtos[2].typeOfWaste);
        this.container3Type = this.getTypeOfWaist(rb.containerDtos[3].typeOfWaste);
        this.selectedRecycleBin = new recycleBin(rb.id, rb.status, rb.longit, rb.latit, rb.street
          , rb.city, rb.zipCode, rb.country, rb.containerDtos);
      } else {
        this.alertService.info("Nenhum ecoponto registado.", "");
      }
    }, (httpError) => {
      try {
        this.alertService.error(Object.values(httpError.error.errors).join(" and "), "");
      } catch (e) {
        if (typeof httpError.error === 'string' || httpError.error instanceof String) {
          this.alertService.error(httpError.error, "");
        } else {
          this.alertService.error(Object.values(httpError.error).join(" and "), "");
        }
      }
    });

  }

  getTypeOfWaist(type: String) {
    switch (type) {
      case 'Undifferentiated':
        return 'Indiferenciado';
      case 'Paper':
        return 'Papel'
      case 'Glass':
        return 'Vidro'
      default:
        return 'Plástico/Metal'
    }
  }

  selectRB(rb: any) {
    this.recBin = rb;
    this.container0Type = this.getTypeOfWaist(rb.containerDtos[0].typeOfWaste);
    this.container1Type = this.getTypeOfWaist(rb.containerDtos[1].typeOfWaste);
    this.container2Type = this.getTypeOfWaist(rb.containerDtos[2].typeOfWaste);
    this.container3Type = this.getTypeOfWaist(rb.containerDtos[3].typeOfWaste);

    this.selectedRecycleBin = new recycleBin(rb.id, rb.status, rb.longit, rb.latit, rb.street
      , rb.city, rb.zipCode, rb.country, rb.containerDtos);

    //Reset all values otherwise every click will increment the previous ocupation values
    this.data = [
      ['Paper', 0, 0, 0, 0],
      ['Glass', 0, 0, 0, 0],
      ['Plastic', 0, 0, 0, 0],
      ['Undif', 0, 0, 0, 0],
    ];
    this.selectedRecycleBin.containers.forEach(container => {
      if (container.typeOfWaste === 'Paper') {
        this.data[0][1] += container.percentageOccupied
      } else if (container.typeOfWaste === 'Glass') {
        this.data[1][1] += container.percentageOccupied
      } else if (container.typeOfWaste === 'Plastic') {
        this.data[2][1] += container.percentageOccupied
      } else if (container.typeOfWaste === 'Undifferentiated') {
        this.data[3][1] += container.percentageOccupied
      }
    });
    this.data = [
      ['Paper', this.data[0][1], 0, 0, 0],
      ['Glass', 0, this.data[1][1], 0, 0],
      ['Plastic', 0, 0, this.data[2][1], 0],
      ['Undif', 0, 0, 0, this.data[3][1]],
    ];
  }

  disableRecycleBin() {
    if(this.recBin.status != "Active"){
      this.alertService.info("O ecoponto selecionado já está desativo","");
      return 
  }
    if (this.selectedRecycleBin._id != "") {
      const dialogRef = this.dialog.open(DisableRecycleBinComponent, {
        width: '500px', height: 'auto'
        , data: this.selectedRecycleBin
      });

      dialogRef.afterClosed().subscribe(result => {
        if (localStorage.getItem("disable") === 'ok') {
          this.restRecycleBins.disableRecycleBin(this.selectedRecycleBin._id).subscribe((res) => {
            this.alertService.operationsSucced("Ecoponto desativado com sucesso!")
            this.selectedRecycleBin.status = "INACTIVE";
            localStorage.removeItem("disable");
            setTimeout(() => {
              location.reload()
            }, 1500);
          }, ((httpError) => {
            localStorage.removeItem("disable");
            try {
              this.alertService.error(Object.values(httpError.error.errors).join(" and "), "");
            } catch (e) {
              if (typeof httpError.error === 'string' || httpError.error instanceof String) {
                this.alertService.error(httpError.error, "");
              } else {
                this.alertService.error(Object.values(httpError.error).join(" and "), "");
              }
            }
          }));
        } else if (localStorage.getItem("disable") === 'cancel') {
          localStorage.removeItem("disable");
        } else {
          localStorage.removeItem("disable");
        }
      });
    } else {
      this.alertService.error("Nenhum ecoponto foi selecionado!", "Operação Inválida");
    }
  }

  addRecycleBin() {
    this.router.navigate(['home/eco/add']);
  }

  ativateRecBin() {
    if(this.recBin.status == "Active"){
        this.alertService.info("O ecoponto selecionado já está ativo","");
        return 
    }

    this.restRecycleBins.activeRecBin(this.recBin.id).subscribe({
      next: (data) => {
        this.alertService.operationsSucced("Ecoponto ativado com sucesso!")
        this.selectedRecycleBin.status = "ACTIVE";
        setTimeout(() => {
          location.reload()
        }, 1500);
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

  updateRecycleBin() {
    if (this.selectedRecycleBin._id != "") {
      const dialogRef = this.dialog.open(UpdateRecycleBinComponent, {
        width: '600px', height: 'auto'
        , data: this.selectedRecycleBin
      });

      dialogRef.afterClosed().subscribe(result => {
        if (localStorage.getItem("updated") === 'ok') {
          let res = JSON.parse(localStorage.getItem("updatedRecycleBin")!);
          console.log(this.selectedRecycleBin._id)
          res.id = this.selectedRecycleBin._id;
          console.log(res)
          this.restRecycleBins.updateRecycleBin(res).subscribe((res) => {
            localStorage.removeItem("updated");
            localStorage.removeItem("updatedRecycleBin");
            this.alertService.operationsSucced("Ecoponto Atualizado!");
            setTimeout(() => {
              location.reload()
            }, 1500);
          }, ((error) => {
            localStorage.removeItem("updated");
            localStorage.removeItem("updatedRecycleBin");
            this.alertService.error("Não foi possivel atualizar ecoponto.", "")
          }));
        } else if (localStorage.getItem("updated") === 'cancel') {
          localStorage.removeItem("updated");
        } else {
          localStorage.removeItem("updated");
        }
      });
    } else {
      this.alertService.error("Nenhum ecoponto foi selecionado!", "Operação Inválida");
    }
  }
}
