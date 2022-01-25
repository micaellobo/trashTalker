import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { recycleBin } from 'src/models/recycleBin';

@Component({
  selector: 'app-disable-recycle-bin',
  templateUrl: './disable-recycle-bin.component.html',
  styleUrls: ['./disable-recycle-bin.component.css']
})
export class DisableRecycleBinComponent implements OnInit {
  selectedRecycleBin!:recycleBin

  constructor(public dialogRef: MatDialogRef<DisableRecycleBinComponent>
    ,@Inject(MAT_DIALOG_DATA) public recycleBin:recycleBin) {
      this.selectedRecycleBin = recycleBin;
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
