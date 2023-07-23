import { Component, Inject, OnInit, ViewChild, OnChanges } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ToolbarService, LinkService, ImageService, HtmlEditorService, QuickToolbarService } from '@syncfusion/ej2-angular-richtexteditor';
import { Detaliirelease } from 'app/models/detaliirelease';
import { EmailDetails } from 'app/models/email-details';
import { EmailService } from 'app/services/email.service';
import * as moment from 'moment';

@Component({
  selector: 'app-submit-email',
  templateUrl: './submit-email.component.html',
  styleUrls: ['./submit-email.component.scss'],
  providers: [ToolbarService, LinkService, ImageService, HtmlEditorService, QuickToolbarService]
})
export class SubmitEmailComponent implements OnInit, OnChanges {

  form: FormGroup;
  emailDetails: EmailDetails = new EmailDetails();

  constructor(public dialogRef: MatDialogRef<SubmitEmailComponent>, @Inject(MAT_DIALOG_DATA) public details: EmailDet, public formBuilder: FormBuilder,
    public router: Router, public emailService: EmailService, public snackBar: MatSnackBar) {

  }

  ngOnChanges(): void {


    this.form.get("sendToText")?.disable();
  }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      sendToText: [''],
      ccMailText: [''],
      richText: ['']
    })

    this.form.controls.sendToText?.setValue("andreearaluca210@gmail.com");
    this.form.controls.ccMailText?.setValue(this.details.ccEmail);
    // this.form.get("richText")?.patchValue('');

    this.form.get("sendToText")?.disable();
  }

  // get formControls() {
  //   return this.form.controls;
  // }

  public tools: object = {
    items: ['Undo', 'Redo', '|',
      'Bold', 'Italic', 'Underline', 'StrikeThrough', '|',
      'FontName', 'FontSize', 'FontColor', 'BackgroundColor', '|',
      'SubScript', 'SuperScript', '|',
      'LowerCase', 'UpperCase', '|',
      'Formats', 'Alignments', '|', 'OrderedList', 'UnorderedList', '|',
      'Indent', 'Outdent', '|', 'CreateLink',
      'Image', '|', 'ClearFormat', 'Print', 'SourceCode', '|', 'FullScreen']
  };
  public iframe: object = { enable: true };
  public height: number = 200;
  public width: number = 500;


  onCancel() {
    this.dialogRef.close();
    this.router.navigate(['/home/home/relfollowup'])
  }

  onSubmit = async () => {
    let mailSubject = "";
    let title = "";
    let subjectEmerg = "";
    console.log(this.form.get("sendToText")?.value)
    console.log(this.form.get("ccMailText")?.value)
    console.log(this.form.get("richText")?.value)
    let newHour = moment(this.details.release.datarelease).format("DD-MM-YYYY hh:mm A");
    let data = moment(this.details.release.datarelease).format("DD-MM-YYYY");
    if (this.details.release.esteurgenta == 1) {
      subjectEmerg = " - EMERGENCY RELEASE"
    }
    if (this.details.release.denumiremediu == "UAT") {
      mailSubject = "[Scheduled] " + "Release on " + this.details.release.denumireaplicatie + ' [' + this.details.release.denumiremediu + '] ' + "- " + newHour + "" + subjectEmerg;
    }else{
      mailSubject = "[Scheduled] " + "Release on " + this.details.release.denumireaplicatie + ' [' + this.details.release.denumiremediu + '] ' + this.details.release.idimpulse + " - " + newHour + "" + subjectEmerg;
    }
    title = "[Scheduled] " + "Release on " + this.details.release.denumireaplicatie
    this.emailDetails.EmailToID = this.form.controls.sendToText?.value;
    this.emailDetails.EmailCC = this.form.controls.ccMailText?.value;
    this.emailDetails.EmailSubject = mailSubject;
    this.emailDetails.Application = this.details.release.denumireaplicatie;
    this.emailDetails.Env = this.details.release.denumiremediu;
    this.emailDetails.Date = data;

    let duration = this.details.release.duratarelease.substr(11,8);
    let downtime = this.details.release.downtime.substr(11,8);
    this.emailDetails.Hour = moment(this.details.release.datarelease).format("HH:mm:ss");
    this.emailDetails.Duration = duration;
    this.emailDetails.Downtime = downtime;
    this.emailDetails.Contents = this.details.release.contentrelease;
    this.emailDetails.AddOn = this.form.controls.richText?.value;
    this.emailDetails.Title = title;

    console.log(this.emailDetails)

    const sendEmail = await this.emailService.postCreateEmail(this.emailDetails).toPromise();
    console.log(sendEmail)
    if (sendEmail == true) {
      this.snackBar.open("Email trimis!", "", { duration: 1000, panelClass: ["snack-style-success"] })
      this.dialogRef.close();
      this.router.navigate(['/home/home/relfollowup'])
    } else {
      this.snackBar.open("Emailul nu a putut fi trimis!", "", { duration: 1000, panelClass: ["snack-style-danger"] })
    }

  }

}

export class EmailDet {
  ccEmail: string = '';
  release: Detaliirelease = new Detaliirelease();
}
