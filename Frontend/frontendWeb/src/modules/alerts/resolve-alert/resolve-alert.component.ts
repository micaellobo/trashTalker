import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { alert } from 'src/models/alert';

@Component({
  selector: 'app-resolve-alert',
  templateUrl: './resolve-alert.component.html',
  styleUrls: ['./resolve-alert.component.css']
})
export class ResolveAlertComponent implements OnInit {
  selectedAlert!:alert;

  constructor(public dialogRef: MatDialogRef<ResolveAlertComponent>
    ,@Inject(MAT_DIALOG_DATA) public alert:alert) {
      this.selectedAlert = alert;
     }

  ngOnInit(): void {
  }

  cancel(): void {
    localStorage.setItem("solved", "cancel");
    this.dialogRef.close();
  }

  confirm():void{
    localStorage.setItem("solved", "ok");
    this.dialogRef.close();
  }

}
