import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { JiraIssue } from 'app/models/Jira/jira-issue';
import { Detaliirelease } from 'app/models/detaliirelease';
import { EmailDetails } from 'app/models/email-details';
import { DuratereleaseService } from 'app/services/duraterelease.service';
import { JiraService } from 'app/services/jira.service';
import { ReleaseService } from 'app/services/release.service';
import * as moment from 'moment';
import { environment } from 'environments/environment';
import { EmailService } from 'app/services/email.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { StatusService } from 'app/services/status.service';

@Component({
  selector: 'app-warning',
  templateUrl: './warning.component.html',
  styleUrls: ['./warning.component.scss']
})
export class WarningComponent implements OnInit {

  action: string;
  newJiraIssue: Array<JiraIssue> = [];
  emailDetails: EmailDetails = new EmailDetails();

  constructor(public dialogRef: MatDialogRef<WarningComponent>,
    @Inject(MAT_DIALOG_DATA) public details: ReleaseDetails, public releaseService: ReleaseService, public durataService: DuratereleaseService,
    public jiraService: JiraService, public emailService: EmailService, public snackBar: MatSnackBar, public statusService: StatusService) { }

  ngOnInit(): void {
    if (this.details.relBtn == "START") {
      this.action = "pornesti"
    }
    else {
      this.action = "opresti"
    }
  }

  onSubmit = async () => {
    if (this.details.release.denumirestatus == "CONFIRMED") {
      if (this.details.release.relstatus == 0) {

        let release = await this.releaseService.getOneReleaseById(this.details.release.idrelease).toPromise();
        release.relstatus = 1;
        await this.releaseService.updateRelease(this.details.release.idrelease, release).toPromise();

        let detaliiDataRelease = await this.durataService.getOneDurataById(this.details.release.iddurata).toPromise();
        detaliiDataRelease.datastart = moment(new Date()).format("yyyy-MM-DD HH:mm:ss");
        detaliiDataRelease.dataend = moment(new Date("1900-01-01 00:00:00")).format("yyyy-MM-DD HH:mm:ss");
        await this.durataService.updateDurata(detaliiDataRelease.iddurata, detaliiDataRelease).toPromise();

        await this.getJiraTickets();

        await this.sendEmail(this.details.release, "[STARTING]")
        this.dialogRef.close();

      } else if (this.details.release.relstatus == 1) {
        let status = await this.statusService.getOneStatusByDen("RELEASED").toPromise();
        let release = await this.releaseService.getOneReleaseById(this.details.release.idrelease).toPromise();
        release.relstatus = 2;
        release.idstatus = status.idstatus;
        await this.releaseService.updateRelease(this.details.release.idrelease, release).toPromise();

        let detaliiDataRelease = await this.durataService.getOneDurataById(this.details.release.iddurata).toPromise();
        detaliiDataRelease.dataend = moment(new Date()).format("yyyy-MM-DD HH:mm:ss");

        let startDate = new Date(detaliiDataRelease.datastart);
        let endDate = new Date();
        let hours = Math.floor(Math.abs(endDate.valueOf() - startDate.valueOf()) / (1000 * 60 * 60) % 24);
        let minutes = Math.floor(Math.abs(endDate.getTime() - startDate.getTime()) / (1000 * 60) % 60);
        let seconds = Math.floor(Math.abs(endDate.getTime() - startDate.getTime()) / (1000) % 60);
        let newHours;
        let newMin;
        let newSec;

        if (hours < 10) {
          newHours = "0" + hours
        } else {
          newHours = hours
        }

        if (minutes < 10) {
          newMin = "0" + minutes
        } else {
          newMin = minutes;
        }

        if (seconds < 10) {
          newSec = "0" + seconds
        } else {
          newSec = seconds
        }

        let newDur = newHours + ":" + newMin + ":" + newSec;
        detaliiDataRelease.durata = this.getDurationAndDowntime(newDur);
        console.log(this.getDurationAndDowntime(newDur))
        this.details.release.duratarelease = newDur;
        await this.durataService.updateDurata(detaliiDataRelease.iddurata, detaliiDataRelease).toPromise();

        await this.getJiraTickets();
        await this.sendEmail(this.details.release, "[RELEASED]")
        this.dialogRef.close();
      }
    }

  }

  onCancel() {
    this.dialogRef.close();
  }

  public getDurationAndDowntime(date: string): string {
    const newDate = "2000-01-01 " + date;
    return moment(newDate).format("YYYY-MM-DD HH:mm:ss");
  }

  getJiraTickets = async () => {
    const jiras = await this.jiraService.getAllIssues(this.details.release.contentrelease).toPromise();
    console.log(jiras)
    if (jiras) {
      jiras.forEach(jira => {
        this.newJiraIssue.push(jira)
      })
    }
    return this.newJiraIssue;

  }

  sendEmail = async (release: Detaliirelease, phase: string) => {
    let contents = "<ul>"
    this.newJiraIssue.forEach(element => {
      contents += "<li>" + element.key + "-" + element.fields.summary + "</li>";
    })
    contents += "</ul>";

    let mailSubject = "";
    let title = phase + "Releases on " + release.denumireaplicatie;
    mailSubject = phase + "Releases on " + release.denumireaplicatie + " [" + release.denumiremediu + "] " + "-" + release.datarelease;

    this.emailDetails.EmailToID = environment.emailTo;
    this.emailDetails.EmailCC = environment.emailTo;
    this.emailDetails.EmailSubject = mailSubject;
    this.emailDetails.Application = release.denumireaplicatie;
    this.emailDetails.Env = release.denumiremediu;
    this.emailDetails.Date = release.datarelease;

    // let duration = this.details.release.duratarelease.substr(11,8);
    // let downtime = this.details.release.downtime.substr(11,8);
    this.emailDetails.Hour = moment(release.datarelease).format("HH:mm:ss");
    this.emailDetails.Duration = release.duratarelease;
    this.emailDetails.Downtime = release.downtime;
    this.emailDetails.Contents = contents;
    this.emailDetails.Title = title;

    const sendStartEmail = await this.emailService.postStartEmail(this.emailDetails).toPromise();
    if (sendStartEmail == true) {
      if(phase == "[RELEASED]"){
      this.snackBar.open("Email trimis! Release-ul s-a incheiat cu success!", "", { duration: 1000, panelClass: ["snack-style-success"] })

      }else{
        this.snackBar.open("Email trimis! Release-ul a inceput cu success!", "", { duration: 1000, panelClass: ["snack-style-success"] })
      }
      
    } else {
      this.snackBar.open("Emailul nu a putut fi trimis!", "", { duration: 1000, panelClass: ["snack-style-danger"] })
    }

  }


}


export class ReleaseDetails {
  relBtn: string = '';
  release: Detaliirelease = new Detaliirelease();
}