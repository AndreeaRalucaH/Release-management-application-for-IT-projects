import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Aplicatii } from 'app/models/aplicatii';
import { AplicatiiService } from 'app/services/aplicatii.service';

@Component({
  selector: 'app-add-aplicatie',
  templateUrl: './add-aplicatie.component.html',
  styleUrls: ['./add-aplicatie.component.scss']
})
export class AddAplicatieComponent implements OnInit {

  form: FormGroup
  appToAdd: Aplicatii = new Aplicatii();
  constructor(public dialogRef: MatDialogRef<AddAplicatieComponent>,public formBuilder: FormBuilder, public appService: AplicatiiService,public snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      appDen: [''],
      emails: [''],
      owner: [''],
      manager: ['']
    })
  }

  onSubmit = async() =>{
    this.appToAdd.denumire = this.form.get("appDen")?.value;
    this.appToAdd.emails = this.form.get("emails")?.value;
    this.appToAdd.ownerproiect = this.form.get("owner")?.value;
    this.appToAdd.managerproiect = this.form.get("manager")?.value;
    this.appToAdd.datacreare = new Date();
    this.appToAdd.datamodificare = new Date();

    console.log(this.appToAdd)

    const addApp = await this.appService.createApp(this.appToAdd).toPromise();
    if(addApp != null){
      this.snackBar.open("Aplicatia a fost creata cu succes!", "", { duration: 2000, panelClass: ["snack-style-success"] })
    }else{
      this.snackBar.open("Aplicatia nu a putut fi creata!", "", { duration: 2000, panelClass: ["snack-style-danger"] })
    }

    this.dialogRef.close();
  }

  onCancel(){
    this.dialogRef.close();
  }

}
