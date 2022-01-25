import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { employee } from 'src/models/employee';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css']
})
export class UpdateComponent implements OnInit {
  constructor(public dialogRef: MatDialogRef<UpdateComponent>
    ,@Inject(MAT_DIALOG_DATA) public emp:employee) { 
    }
  ngOnInit(): void {
  }

  cancel(): void {
    localStorage.setItem("updated", "cancel");
    this.dialogRef.close();
  }

  confirm():void{
    localStorage.setItem("updated", "ok");
    localStorage.setItem("updatedEmployee", JSON.stringify(this.emp));
    this.dialogRef.close();
  }

}
