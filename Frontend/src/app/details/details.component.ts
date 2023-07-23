import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { CommonMethods } from 'app/common.methods';
import { JiraIssue } from 'app/models/Jira/jira-issue';
import { Detaliirelease } from 'app/models/detaliirelease';
import { DuratereleaseService } from 'app/services/duraterelease.service';
import { JiraService } from 'app/services/jira.service';
import { ReleaseService } from 'app/services/release.service';
import { UtilizatoriService } from 'app/services/utilizatori.service';
import * as moment from 'moment';
import { weekNumberSun } from 'weeknumber';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {
  form: FormGroup
  newJiraIssue: Array<JiraIssue> = [];
  dataSourceJira = new MatTableDataSource(this.newJiraIssue)
  displayColumns: string[] = ["name", "key", "summary"];
  disableUpdateBtn: boolean = false;

  constructor(public dialogRef: MatDialogRef<DetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public details: ReleaseDetails, public formBuilder: FormBuilder,public jiraService: JiraService, public releaseService: ReleaseService,
    public durationService: DuratereleaseService, public userService: UtilizatoriService) { }

  ngOnInit(): void {
    console.log(this.details)
    this.initForm();
    this.getJiraTickets();
  }

  initForm() {
    const newDate = moment(this.details.release.datarelease).format("YYYY-MM-DD");
    const newHour = moment(this.details.release.datarelease).format("HH:mm:ss");
    let urgenta = '';
    if (this.details.release.esteurgenta == 0) {
      urgenta = "NU"
    } else {
      urgenta = "DA"
    }
    this.form = this.formBuilder.group({
      appName: [this.details.release.denumireaplicatie],
      date: [newDate],
      hour: [newHour],
      duration: [this.details.release.duratarelease],
      downtime: [this.details.release.downtime],
      enh: [this.details.release.imbunatatiri],
      bug: [this.details.release.bugs],
      emerg: [urgenta],
      content: [this.details.release.contentrelease],
      creator: [this.details.release.creator]
    })

    this.disableOrEnableFields();
  }

  disableOrEnableFields(){
    if(this.details.release.denumirestatus == "CONFIRMED"){
      if(this.details.isAdmin == true || this.details.release.denumiremediu == "UAT"){
        this.form.controls['date'].enable();
        this.form.controls['hour'].enable();
        this.form.controls['duration'].enable();
        this.form.controls['downtime'].enable();
        this.form.controls['emerg'].enable();
        this.form.controls['content'].enable();
        this.disableUpdateBtn = false;
      } else {
        this.form.controls['date'].disable();
        this.form.controls['hour'].disable();
        this.form.controls['duration'].disable();
        this.form.controls['downtime'].disable();
        this.form.controls['emerg'].disable();
        this.form.controls['content'].disable();
        this.disableUpdateBtn = true;
      }
    } else if (this.details.release.denumirestatus == "RELEASED" || this.details.release.denumirestatus == "DELIVERED" || this.details.release.denumirestatus == "CANCELLED" ||
    (this.details.release.brvpath != "" && this.details.release.testpath != "")) {
      this.form.controls['date'].disable();
      this.form.controls['hour'].disable();
      this.form.controls['duration'].disable();
      this.form.controls['downtime'].disable();
      this.form.controls['emerg'].disable();
      this.form.controls['content'].disable();
      this.disableUpdateBtn = true;
    } else {
      this.form.controls['date'].enable();
      this.form.controls['hour'].enable();
      this.form.controls['duration'].enable();
      this.form.controls['downtime'].enable();
      this.form.controls['emerg'].enable();
      this.form.controls['content'].enable();
      this.disableUpdateBtn = false;
    }
  }

  getJiraTickets = async () => {
    const jiras = await this.jiraService.getAllIssues(this.details.release.contentrelease).toPromise();
    console.log(jiras)
    if(jiras){
      jiras.forEach(jira => {
        this.newJiraIssue.push(jira)
      })
    }
    console.log(this.newJiraIssue)
    this.dataSourceJira.data = this.newJiraIssue as JiraIssue[];
    console.log(this.dataSourceJira)
  }

  getMonthFromDate(date: Date): string{
    const year = date.getFullYear();
    const month = date.getMonth() + 1;
    let newMonth = month.toString();
    if(month < 10){
      newMonth = "0" + month;
    }else{
      newMonth = month.toString();
    }
    const yearMonth = year + '-' + newMonth
    return yearMonth;

  }

  getDateWithTime(date: Date, hour: string): string{
    const day = date.getDate();
    const month = date.getMonth() + 1;
    const year = date.getFullYear();
    
    const fullDate = year + '-' + month + '-' + day;
    const newDate = fullDate + " " + hour;
    const dateReq = moment(newDate).format("YYYY-MM-DD HH:mm:ss");

    return dateReq;
  }

  getDurationAndDowntime(date: string): string{
    const newDate = "2000-01-01 " + date;
    return moment(newDate).format("YYYY-MM-DD HH:mm:ss");
  }

  getBugsNumber(issues: JiraIssue[]): number {
    let bugs = 0;
    if(issues){
      issues.forEach(issues => {
        if(issues.fields.issuetype.name == "Bug"){
          bugs++;
        }
      })
    }
    return bugs;
   
  }

  getNonBugsNumber(issues: JiraIssue[]): number {
    let nonbugs = 0;
    if(issues){
      issues.forEach(issues => {
        if(issues.fields.issuetype.name != "Bug"){
          nonbugs++;
        }
      })
    }
    return nonbugs;
   
  }

  onUpdate = async () => {
    let date = this.form.get("date")?.value;
    let hour = this.form.get("hour")?.value;
    let duration = this.form.get("duration")?.value;
    let downtime = this.form.get("downtime")?.value;
    let emerg = this.form.get("emerg")?.value;
    let contents = this.form.get("content")?.value;

    const release = await this.releaseService.getOneReleaseById(this.details.release.idrelease).toPromise();
    const durataTable = await this.durationService.getOneDurataById(this.details.release.iddurata).toPromise();
    const jiras = await this.jiraService.getAllIssues(contents).toPromise();

    durataTable.datarelease = this.getDateWithTime(new Date(date), hour);
    durataTable.durata = this.getDurationAndDowntime(duration);
    durataTable.downtime = this.getDurationAndDowntime(downtime);
    durataTable.luna = this.getMonthFromDate(new Date(date));
    durataTable.saptamana = weekNumberSun(new Date(date)).toString();

    if(emerg == "NU"){
      release.esteurgenta = 0;
    }else {
      release.esteurgenta = 1;
    }
    release.contentrelease = contents;
    release.bugs = this.getBugsNumber(jiras);
    release.imbunatatiri = this.getNonBugsNumber(jiras);

    await this.releaseService.updateRelease(this.details.release.idrelease, release).toPromise();
    await this.durationService.updateDurata(this.details.release.iddurata, durataTable).toPromise();
    console.log(release, durataTable)

    this.dialogRef.close();

  }

  onCancel(){
    this.dialogRef.close();
  }


}

export class ReleaseDetails {
  release: Detaliirelease = new Detaliirelease();
  isAdmin: boolean = false;
}