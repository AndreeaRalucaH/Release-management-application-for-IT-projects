import { Component, Input, OnInit,Inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ReleaseService } from 'app/services/release.service';
import { StatusService } from 'app/services/status.service';

@Component({
  selector: 'app-workflow',
  templateUrl: './workflow.component.html',
  styleUrls: ['./workflow.component.scss']
})
export class WorkflowComponent implements OnInit {

  form: FormGroup
  constructor(public dialogRef: MatDialogRef<WorkflowComponent>,
    @Inject(MAT_DIALOG_DATA) public statusDetails: StatusDetails, public formBuilder: FormBuilder, public statusServ: StatusService, public releaseService: ReleaseService){ }

  ngOnInit(): void {
    console.log(this.statusDetails)
    this.form = this.formBuilder.group({
      currentStatus: [this.statusDetails.statusName],
      newStatus: ['']
    })

    this.form.get("currentStatus")?.disable();
  }

  onSubmit = async () => {
    if(this.form.valid){
      const release =  await this.releaseService.getOneReleaseById(this.statusDetails.idRelease).toPromise();
      const statusDen = await this.statusServ.getOneStatusByDen(this.form.get("newStatus")?.value).toPromise();
      release.idstatus = statusDen.idstatus;
      await this.releaseService.updateRelease(this.statusDetails.idRelease, release).toPromise();
      this.dialogRef.close();
    }
  }

  onCancel(){
    this.dialogRef.close();
  }

}


export class StatusDetails{
  statusName: string = '';
  statusNames: string[] = [];
  idRelease: number = 0;
}