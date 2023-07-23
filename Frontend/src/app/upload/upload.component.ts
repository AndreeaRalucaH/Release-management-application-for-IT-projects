import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AddDocumentsService } from 'app/services/add-documents.service';
import { ReleaseService } from 'app/services/release.service';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss']
})
export class UploadComponent implements OnInit {

  fileName = null;
  file = new File([], "");

  constructor(public dialogRef: MatDialogRef<UploadComponent>, @Inject(MAT_DIALOG_DATA) public impDet: ImpulseDetails, public uploadService: AddDocumentsService,
  public snackBar: MatSnackBar, public releaseService: ReleaseService) { }

  ngOnInit(): void {
  }

  onUpload = async() => {
    console.log(this.file)
    if(this.file.size != 0){
      const result = await this.uploadService.uploadTests(this.file, this.impDet.idImpulse).toPromise();
      if(result.extension != null){
        console.log(result)
        let newRelease = await this.releaseService.getOneReleaseById(Number(this.impDet.idRelease)).toPromise();
        newRelease.testpath = result.savePath;
        await this.releaseService.updateRelease(newRelease.idrelease, newRelease).toPromise();
        console.log(newRelease)
        this.snackBar.open("Fisier adugat cu success!", "", {duration: 2000, panelClass:["snack-style-success"]})
        this.dialogRef.close();
      }else{
        this.snackBar.open("Fisierul nu a putut fi adugat!", "", {duration: 2000, panelClass:["snack-style-danger"]})
      }
      
    }else {
      this.snackBar.open("Alege un fisier!", "", {duration: 2000, panelClass:["snack-style-danger"]})
    }
  }

  onFileSelected(event: any){
    this.file = event.target.files[0];
    console.log(this.file)
    if(this.file){
      this.fileName = this.file.name;
    }else{
      this.fileName = null;
    }
  }

  onClose(){
    this.fileName = null;
    this.file = {} as File;
    this.dialogRef.close()
  }

}

export class ImpulseDetails {
  idImpulse: string = '';
  idRelease: string = '';
}
