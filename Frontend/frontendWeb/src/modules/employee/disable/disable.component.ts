import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { employee } from 'src/models/employee';

@Component({
  selector: 'app-disable',
  templateUrl: './disable.component.html',
  styleUrls: ['./disable.component.css']
})
export class DisableComponent implements OnInit {
  selectedEmployee!:employee

  constructor(public dialogRef: MatDialogRef<DisableComponent>
    ,@Inject(MAT_DIALOG_DATA) public employee:employee) { 
      this.selectedEmployee = employee;
    }

  ngOnInit(): void {
  }

  cancel(): void {
    localStorage.setItem("disable", "cancel");
    this.dialogRef.close();
  }

  confirm():void{
    localStorage.setItem("disable", "ok");
    this.dialogRef.close();
  }

}
