import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router, ActivatedRoute } from '@angular/router';
import { alert } from 'src/models/alert';
import { AlertsService } from 'src/services/alerts/alerts.service';
import { AlertService } from 'src/services/login/alert/alert.service';
import { ResolveAlertComponent } from '../resolve-alert/resolve-alert.component';

@Component({
  selector: 'app-show-all-alerts',
  templateUrl: './show-all-alerts.component.html',
  styleUrls: ['./show-all-alerts.component.css']
})
export class ShowAllAlertsComponent implements OnInit {

  myControl = new FormControl();
  options: any[] = [];
  listAlerts:any[]=[];
  selectedAlert: alert=new alert("","","", new Date(2021, 12, 20, 10, 11, 11, 1), "", "");
  
  constructor(private router:Router,private route:ActivatedRoute,
    private restAlerts:AlertsService,private alertService: AlertService,
    public dialog: MatDialog) { }
  
  ngOnInit(): void {
    this.restAlerts.showAlerts().subscribe((listAlerts:any[])=>{
      if(listAlerts.length > 0){
        this.listAlerts=listAlerts;
        let a = listAlerts[0];
        this.selectedAlert = new alert(a.id, a.alertStatus, a.alertType, a.date, a.issue, a.employeeId);
      }
      else{
        this.alertService.info("De momento não existem notificações ativas!","");
      }
    },(error:Error)=>{
    });
  }
 
  selectAlert(a:any){
    this.selectedAlert = new alert(a.id, a.alertStatus, a.alertType, a.date, a.issue, a.employeeId);
  }

  resolveAlert(){
    if(this.selectedAlert._id != ""){
      const dialogRef = this.dialog.open(ResolveAlertComponent, {
        width: '500px', height: 'auto'
        , data: this.selectAlert
      });
        
        dialogRef.afterClosed().subscribe(result => {
          if (localStorage.getItem("solved")==='ok') {
                this.restAlerts.resolveAlert(this.selectedAlert._id).subscribe((res)=>{
                  this.selectedAlert.alertStatus = "SOLVED";
                  localStorage.removeItem("solved");
                  setTimeout(()=>location.reload(),300);
                },((error)=>{
                  localStorage.removeItem("solved");
                }));
            }
            else if(localStorage.getItem("solved")==='cancel') {
              localStorage.removeItem("solved");
            }
            else{
              localStorage.removeItem("solved");
            }
        });
    }else{
        this.alertService.error("Nenhum alerta foi selecionado!","Operação inválida");
    }
  }
}


