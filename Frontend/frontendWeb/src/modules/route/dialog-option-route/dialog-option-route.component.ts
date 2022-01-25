import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { DialogAutomaticRouteComponent } from '../dialog-automatic-route/dialog-automatic-route.component';

@Component({
  selector: 'app-dialog-option-route',
  templateUrl: './dialog-option-route.component.html',
  styleUrls: ['./dialog-option-route.component.css']
})
export class DialogOptionRouteComponent implements OnInit {

  constructor(private router:Router,
    public dialogRef: MatDialogRef<DialogOptionRouteComponent>
    ,public dialog: MatDialog) { }

  ngOnInit(): void {
  }

  openManual(){
    this.dialogRef.close();
    this.router.navigate(['home/routes/manual']);
  }

  openAutomatic(){
    this.dialogRef.close();
    const dialogRefAutomatic = this.dialog.open(DialogAutomaticRouteComponent, {
      width: '800px', height: '350px'});
  }
}
