import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AplicatiiService } from 'app/services/aplicatii.service';
import { MediiService } from 'app/services/medii.service';
import { Router } from '@angular/router';
import { Release } from 'app/models/release';
import { Duraterelease } from 'app/models/duraterelease';
import { weekNumberSun } from 'weeknumber';
import * as moment from 'moment';
import { DuratereleaseService } from 'app/services/duraterelease.service';
import { StatusService } from 'app/services/status.service';
import jwt_decode from 'jwt-decode';
import { UtilizatoriService } from 'app/services/utilizatori.service';
import { Utilizatori } from 'app/models/utilizatori';
import { JiraService } from 'app/services/jira.service';
import { JiraIssue } from 'app/models/Jira/jira-issue';
import { ReleaseService } from 'app/services/release.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Detaliirelease } from 'app/models/detaliirelease';
import { MatTableDataSource } from '@angular/material/table';
import { DetaliireleaseService } from 'app/services/detaliirelease.service';
import { MatDialog } from '@angular/material/dialog';
import { SubmitEmailComponent } from 'app/submit-email/submit-email.component';
import { ImpulseService } from 'app/services/impulse.service';
import { ImpulseCreate } from 'app/models/impulse-create';
import { Impulse } from 'app/models/impulse';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

  namesApp: string[] = [];
  namesMedii: string[] = [];
  hourList: string[] = [];
  newHourList: string[] = [];
  form: FormGroup;
  newRelease: Release = new Release();
  newDate: Duraterelease = new Duraterelease();
  userEmail: Utilizatori;
  impulseCreate: ImpulseCreate = new ImpulseCreate();
  impulse: Impulse = new Impulse();

  viewRelDetails: Detaliirelease[] = [];
  displayedColumns: string[] = ["denumiremediu", "datarelease", "denumireaplicatie"];
  dataSource = new MatTableDataSource(this.viewRelDetails);

  constructor(public formBuilder: FormBuilder, public mediuService: MediiService, public appService: AplicatiiService, public router: Router,
    public dataService: DuratereleaseService, public statusService: StatusService, public userService: UtilizatoriService, public jiraService: JiraService,
    public relService: ReleaseService, public snackBar: MatSnackBar, public detaliiReleaseService: DetaliireleaseService, public dialog: MatDialog,
    public impulseService: ImpulseService) {
    this.getAllApps();
    this.getAllMedii();
  }

  ngOnInit() {
    this.getUserDetails();

    this.form = this.formBuilder.group({
      denMediu: [''],
      denApp: [''],
      data: [''],
      ora: [''],
      durata: [''],
      downtime: [''],
      content: [''],
      urgenta: ['NO'],
    })

    this.getAllHours();

  }

  getUserDetails = async () => {
    const userTkn = localStorage.getItem("googleAuth");
    const userDetails = jwt_decode(userTkn);
    // @ts-ignore
    this.userEmail = await this.userService.getOneUserByDen(userDetails.email).toPromise();
  }

  get formControls() {
    return this.form.controls;
  }

  getAllHours() {
    var x = 5;
    var tt = 0;

    for (var i = 0; tt < 24 * 60; i++) {
      var hh = Math.floor(tt / 60);
      var mm = (tt % 60);
      this.hourList[i] = ("0" + (hh % 24)).slice(-2) + ':' + ("0" + mm).slice(-2);
      tt = tt + x;
    }
    this.newHourList = this.hourList;
  }

  getAllApps = async () => {
    const apps = await this.appService.getAllApps().toPromise();
    apps.forEach(app => {
      this.namesApp.push(app.denumire);
    })
  }

  getAllMedii = async () => {
    const medii = await this.mediuService.getAllMedii().toPromise();
    medii.forEach(mediu => {
      this.namesMedii.push(mediu.denumire);
    })
  }

  onCancel() {
    this.router.navigate(['/home/home/relfollowup'])
  }

  public getMonthFromDate(date: Date): string {
    const year = date.getFullYear();
    const month = date.getMonth() + 1;
    let newMonth = month.toString();
    if (month < 10) {
      newMonth = "0" + month;
    } else {
      newMonth = month.toString();
    }
    const yearMonth = year + '-' + newMonth
    return yearMonth;

  }

  public getDateWithTime(date: Date, hour: string): string {
    const day = date.getDate();
    const month = date.getMonth() + 1;
    const year = date.getFullYear();

    const fullDate = year + '-' + month + '-' + day;
    const newDate = fullDate + " " + hour;
    const dateReq = moment(newDate).format("YYYY-MM-DD HH:mm:ss");

    return dateReq;
  }

  public getDurationAndDowntime(date: string): string {
    const newDate = "2000-01-01 " + date;
    return moment(newDate).format("YYYY-MM-DD HH:mm:ss");
  }

  public getBugsNumber(issues: JiraIssue[]): number {
    let bugs = 0;
    if (issues) {
      issues.forEach(issues => {
        if (issues.fields.issuetype.name == "Bug") {
          bugs++;
        }
      })
    }
    return bugs;

  }

  public getNonBugsNumber(issues: JiraIssue[]): number {
    let nonbugs = 0;
    if (issues) {
      issues.forEach(issues => {
        if (issues.fields.issuetype.name != "Bug") {
          nonbugs++;
        }
      })
    }
    return nonbugs;

  }

  checkDate = async ($event: any) => {
    this.getAllHours();
    this.viewRelDetails = [];

    const matchingRels = await this.detaliiReleaseService.getMatchDate($event.target.value).toPromise();
    console.log(matchingRels)

    matchingRels.forEach(rel => {
      if (this.form.get("denMediu").value != "") {
        if (rel.denumiremediu == this.form.get("denMediu").value) {
          this.viewRelDetails.push(rel);
        }
      }
    })

    this.dataSource.data = this.viewRelDetails as Detaliirelease[];
    console.log(this.dataSource.data)
  }

  changeEnv() {
    this.form.get("content")?.patchValue("");

    this.form.get("data")?.patchValue("");
    this.form.get("ora")?.patchValue("");
    this.viewRelDetails = [];
  }

  onSubmit = async () => {
    if (this.form.valid) {
      //duraterelease
      this.newDate.saptamana = weekNumberSun(new Date(this.form.get("data")?.value)).toString();
      this.newDate.luna = this.getMonthFromDate(new Date(this.form.get("data")?.value));
      this.newDate.datarelease = this.getDateWithTime(new Date(this.form.get("data")?.value), this.form.get("ora")?.value);
      this.newDate.durata = this.getDurationAndDowntime(this.form.get("durata")?.value);
      this.newDate.downtime = this.getDurationAndDowntime(this.form.get("downtime")?.value);
      this.newDate.datastart = moment(new Date()).format("yyyy-MM-DD HH:mm:ss");
      this.newDate.dataend = moment(new Date()).format("yyyy-MM-DD HH:mm:ss");
      this.newDate.datacreare = new Date();
      this.newDate.datamodificare = new Date();
      //post
      const newDateForRel = await this.dataService.createDurata(this.newDate).toPromise();


      //ids
      const idStatus = await this.statusService.getOneStatusByDen("INITIATE").toPromise()
      const idAplicatie = await this.appService.getOneAppByDen(this.form.get("denApp")?.value).toPromise();
      const idMediu = await this.mediuService.getOneMediuByDen(this.form.get("denMediu")?.value).toPromise();

      //jiras
      const jiras = await this.jiraService.getAllIssues(this.form.get("content")?.value).toPromise();

      //release
      this.newRelease.idaplicatie = idAplicatie.idaplicatie;
      this.newRelease.idmediu = idMediu.idmediu;
      this.newRelease.idstatus = idStatus.idstatus;
      this.newRelease.iddurata = newDateForRel.iddurata;
      this.newRelease.idutilizator = this.userEmail.idutilizator;
      this.newRelease.imbunatatiri = this.getNonBugsNumber(jiras);
      this.newRelease.bugs = this.getBugsNumber(jiras);
      this.newRelease.esteurgenta = this.form.get("urgenta")?.value == "NU" ? 0 : 1;
      this.newRelease.contentrelease = this.form.get("content")?.value;
      this.newRelease.comentarii = '';
      this.newRelease.brvpath = '';
      this.newRelease.testpath = '';
      this.newRelease.datacreare = new Date();
      this.newRelease.datamodificare = new Date();
      //post
      const newAddRelease = await this.relService.createRelease(this.newRelease).toPromise();

      if (this.form.get("denMediu")?.value == "PROD") {
        //impulse
        this.impulseCreate.description = "Release for " + this.form.get("denApp")?.value + " with contents: " + this.form.get("content")?.value;
        this.impulseCreate.short_description = "Release for " + this.form.get("denApp")?.value;
        this.impulseCreate.assignment_group = "Analytics Settings Managers";
        this.impulseCreate.business_service = this.form.get("denApp")?.value;
        this.impulseCreate.start_date = this.form.get("data")?.value;
        this.impulseCreate.end_date = this.form.get("data")?.value;
        //post
        const impulseRes = await this.impulseService.createImpulseChg(this.impulseCreate).toPromise();
        console.log(impulseRes)
        if (impulseRes.number.display_value != null) {
          this.impulse.idrelease = newAddRelease.idrelease;
          this.impulse.idimpulse = impulseRes.number.display_value;
          this.impulse.sysidimpulse = '';
          this.impulse.idtask = '';
          this.impulse.datacreare = new Date();
          this.impulse.datamodificare = new Date();
          const impulseID = await this.impulseService.createImpulse(this.impulse).toPromise();
          console.log(impulseID);
        }
      }


      const newDetaliiRel = await this.detaliiReleaseService.getOneDetaliiRelById(newAddRelease.idrelease).toPromise();
      console.log(newAddRelease)
      console.log(jiras)

      if (newAddRelease) {
        this.snackBar.open("Release creat cu succes!", "", { duration: 1000, panelClass: ["snack-style-success"] })
        this.dialog.open(SubmitEmailComponent, {
          width: '600px',
          data: { ccEmail: idAplicatie.emails, release: newDetaliiRel }
        })

      }





    }
  }



}
