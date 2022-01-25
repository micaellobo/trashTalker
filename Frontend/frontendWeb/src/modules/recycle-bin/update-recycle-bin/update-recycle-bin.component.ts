import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { recycleBin } from 'src/models/recycleBin';
import { RecyclerBinService } from 'src/services/recyclerBin/recycler-bin.service';

@Component({
  selector: 'app-update-recycle-bin',
  templateUrl: './update-recycle-bin.component.html',
  styleUrls: ['./update-recycle-bin.component.css']
})
export class UpdateRecycleBinComponent implements OnInit {
  constructor(public dialogRef: MatDialogRef<UpdateRecycleBinComponent>
    ,@Inject(MAT_DIALOG_DATA) public rb:recycleBin) { 
    }
    
  ngOnInit(): void {
  }

  cancel(): void {
    localStorage.setItem("updated", "cancel");
    this.dialogRef.close();
  }

  confirm():void{
    localStorage.setItem("updated", "ok");
    localStorage.setItem("updatedRecycleBin", JSON.stringify(this.rb));
    this.dialogRef.close();
  }
}
