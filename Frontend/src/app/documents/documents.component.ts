import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { DetailsComponent } from 'app/details/details.component';
import { JiraContentsExcel } from 'app/models/Jira/jira-contents-excel';
import { JiraIssue } from 'app/models/Jira/jira-issue';
import { Aplicatii } from 'app/models/aplicatii';
import { BrvDetails } from 'app/models/brv-details';
import { Detaliirelease } from 'app/models/detaliirelease';
import { AddDocumentsService } from 'app/services/add-documents.service';
import { AplicatiiService } from 'app/services/aplicatii.service';
import { DetaliireleaseService } from 'app/services/detaliirelease.service';
import { JiraService } from 'app/services/jira.service';
import { ReleaseService } from 'app/services/release.service';
import { UploadComponent } from 'app/upload/upload.component';
import * as moment from 'moment';

@Component({
  selector: 'app-documents',
  templateUrl: './documents.component.html',
  styleUrls: ['./documents.component.scss']
})
export class DocumentsComponent implements OnInit {

  form: FormGroup
  newJiraIssue: Array<JiraIssue> = [];
  dataSourceJira = new MatTableDataSource(this.newJiraIssue)
  displayedColumns: string[] = ["key", "summary", "name", "nameStatus", "displayName", "created"];
  noJiras: boolean = false;
  brvDetails: BrvDetails = new BrvDetails();

  constructor(public dialogRef: MatDialogRef<DetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public details: ReleaseDetails, public formBuilder: FormBuilder, public jiraService: JiraService, public dialog: MatDialog,
    public documentService: AddDocumentsService, public snackBar: MatSnackBar, public releaseService: ReleaseService, public detaliiRelease: DetaliireleaseService) { }


  ngOnInit(): void {
    console.log(this.details)
    this.initForm();
    this.getJiraTickets()
  }

  get formControls() {
    return this.form.controls;
  }


  initForm() {
    //const appDet = await this.appService.getOneAppByDen(this.details.release.denumireaplicatie).toPromise();
    const newDate = moment(this.details.release.datarelease).format("yyyy-MM-DD hh:mm:ss A");
    console.log(this.details.release.idimpulse)
    console.log(this.details.release.creator)
    this.form = this.formBuilder.group({
      date: [newDate],
      idImp: [this.details.release.idimpulse],
      creator: [this.details.release.creator],
      appName: [this.details.release.denumireaplicatie],
      propName: [this.details.appDet.ownerproiect],
      respName: [this.details.appDet.managerproiect],
      dataAprobProp: [moment(new Date()).format("yyyy-MM-DD hh:mm:ss A")],
      dataAprobResp: [moment(new Date()).format("yyyy-MM-DD hh:mm:ss A")],
      prop: ["Proprietar proiect"],
      resp: ["Responsabil IT"]
    })

  }

  getJiraTickets = async () => {
    console.log(this.details.release.contentrelease)
    const jiras = await this.jiraService.getAllIssues(this.details.release.contentrelease).toPromise();
    console.log(jiras)
    if (jiras) {
      jiras.forEach(jira => {
        jira.fields.created = moment(jira.fields.created).format("yyyy-MM-DD hh:mm:ss A")
        this.newJiraIssue.push(jira)
      })
    }
    if (this.newJiraIssue.length == 0) {
      this.noJiras = true;
    } else {
      this.noJiras = false;
    }
    console.log(this.newJiraIssue)
    this.dataSourceJira.data = this.newJiraIssue as JiraIssue[];
    console.log(this.dataSourceJira)
  }

  genereazaBrv = async () => {
    console.log(this.form.get("idImp").value)
    this.brvDetails.idImpulse = this.form.get("idImp").value;
    this.brvDetails.creationDate = moment(new Date(this.form.get("date").value)).format("yyyy-MM-DD HH:mm:ss");
    this.brvDetails.author = this.form.get("creator").value;
    this.brvDetails.application = this.form.get("appName").value;
    this.brvDetails.poApp = this.form.get("propName").value;
    this.brvDetails.itApp = this.form.get("respName").value;
    this.brvDetails.poDate = moment(new Date(this.form.get("dataAprobProp").value)).format("yyyy-MM-DD HH:mm:ss");
    this.brvDetails.itDate = moment(new Date(this.form.get("dataAprobResp").value)).format("yyyy-MM-DD HH:mm:ss");
    this.dataSourceJira.data.forEach(jira => {
      let newJiraExcel = new JiraContentsExcel();

      newJiraExcel.ticketId = jira.key;
      newJiraExcel.type = jira.fields.issuetype.name;
      newJiraExcel.summary = jira.fields.summary;
      newJiraExcel.requestor = jira.fields.creator.displayName;
      newJiraExcel.prioritizationDate = jira.fields.created;

      this.brvDetails.jiraContents.push(newJiraExcel)
    })

    console.log(this.brvDetails)
    const brvAdded = await this.documentService.createBrvDoc(this.brvDetails).toPromise();
    console.log(brvAdded)

    if (brvAdded.docAdded == true) {
      let newRelease = await this.releaseService.getOneReleaseById(Number(this.details.release.idrelease)).toPromise();
      newRelease.brvpath = brvAdded.filePath;
      await this.releaseService.updateRelease(newRelease.idrelease, newRelease).toPromise();
      this.snackBar.open("Fisier generat cu success!", "", { duration: 2000, panelClass: ["snack-style-success"] })
      this.dialogRef.close();
    } else {
      this.snackBar.open("Fisierul nu a putut fi generat!", "", { duration: 2000, panelClass: ["snack-style-danger"] })
    }
  }

  incarcaDoc() {
    const dialogUpload = this.dialog.open(UploadComponent, {
      width: "400px",
      data: { idImpulse: this.details.release.idimpulse, idRelease: this.details.release.idrelease }
    })
    dialogUpload.afterClosed().subscribe(res => {
      this.dialogRef.close();
    })
  }

  downloadBrv = async ()=>{
    const release = await this.detaliiRelease.getOneDetaliiRelById(Number(this.details.release.idrelease)).toPromise();
    console.log(release.brvpath)
    const fileName = "[" + release.idimpulse  + "]" + " BRV.xlsx"
    const filePath = `assets/uploads/${fileName}`; 
    console.log(filePath)
    const link = document.createElement('a');
    link.href = filePath;
    link.download = fileName;
    link.target = '_blank';
    link.click();
  }

  downloadTests = async () =>{
    const release = await this.detaliiRelease.getOneDetaliiRelById(Number(this.details.release.idrelease)).toPromise();
    console.log(release.testpath)
    const regex = /.*\\(.*)/;
    const match = regex.exec(release.testpath);
    release.testpath = match ? match[1] : '';
    const fileName = release.testpath
    const filePath = `assets/uploads/${fileName}`; 
    console.log(filePath)
    const link = document.createElement('a');
    link.href = filePath;
    link.download = fileName;
    link.target = '_blank';
    link.click();
  }

}

export class ReleaseDetails {
  release: Detaliirelease = new Detaliirelease();
  isAdmin: boolean = false;
  isBrv: boolean = false;
  isTest: boolean = false;
  appDet: Aplicatii = new Aplicatii();
}
