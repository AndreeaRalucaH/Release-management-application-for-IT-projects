import { Component, ElementRef, OnInit, ViewChild, ViewChildren, QueryList} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { Detaliirelease } from 'app/models/detaliirelease';
import { DetaliireleaseService } from 'app/services/detaliirelease.service';
import { StatusService } from 'app/services/status.service';
import { UtilizatoriService } from 'app/services/utilizatori.service';
import { WorkflowComponent } from 'app/workflow/workflow.component';
import * as moment from 'moment';
import { CommonMethods } from 'app/common.methods';
import * as XLSX from 'xlsx';
import { WarningComponent } from 'app/warning/warning.component';
import { Clipboard } from '@angular/cdk/clipboard';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DetailsComponent } from 'app/details/details.component';
import { DocumentsComponent } from 'app/documents/documents.component';
import { AplicatiiService } from 'app/services/aplicatii.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  @ViewChild('TABLE') table: ElementRef;
  @ViewChildren(MatTable) matTables : QueryList<MatTable<any>>;
  viewDetailsRelease: Detaliirelease[] = [];
  displayedColumns: string [] = [];
  displayedColumnsUAT: string[] = ['denumireaplicatie', 'datarelease', 'duratarelease', 'downtime', 'contentrelease','imbunatatiri', 'bugs', 'esteurgenta', 'denumirestatus', 'workflow', 'release'];
  displayedColumnsPROD: string[] = ['denumireaplicatie', 'datarelease', 'duratarelease', 'downtime','contentrelease', 'imbunatatiri', 'bugs', 'esteurgenta', 'denumirestatus','idimpulse','brv','test', 'workflow', 'release'];
  dataSource = new MatTableDataSource(this.viewDetailsRelease);
  statusNames: string[] = [];
  tableName: string = '';
  isProd: boolean;
  isUat: boolean = true;
  isAdmin: boolean = false;
  showContentsExcel: boolean = true;
  commonMethods: CommonMethods = new CommonMethods();
  isBrv: boolean = false;
  isTest: boolean = false;

  constructor(public detaliiService: DetaliireleaseService, public dialog: MatDialog, public statusService: StatusService, 
    public userService: UtilizatoriService, public clipBoard: Clipboard, public snackBar: MatSnackBar,  public appService: AplicatiiService) { }

  ngOnInit() {
    this.getAllDetailsReleases("UAT")
    this.checkIsAdmin();
  }

  checkIsAdmin = async () => {
    this.isAdmin = await this.commonMethods.checkIsAdmin(this.userService);
    console.log(this.isAdmin)
  }

  getAllDetailsReleases = async (envName: string) => {
    if(envName == "PROD"){
      this.tableName = "PROD Environment"
      this.isProd = true;
      this.displayedColumns = this.displayedColumnsPROD;
    }else{
      this.tableName = "UAT Environment"
      this.displayedColumns = this.displayedColumnsUAT;
    }
    this.dataSource.data = [];
    this.viewDetailsRelease = [];
    const allDetails = await this.detaliiService.getAllDetaliiRel().toPromise();
    allDetails.forEach(rel => {
      if (rel.denumiremediu == envName) {
        const newData = this.formatData(rel.datarelease)
        const newDurata = this.formatDurata(rel.duratarelease)
        const newDowntime = this.formatDurata(rel.downtime)
        rel.datarelease = newData;
        rel.duratarelease = newDurata;
        rel.downtime = newDowntime;
        this.viewDetailsRelease.push(rel)

        if(rel.denumirestatus == "CONFIRMED" && rel.relstatus == 0){
          rel.relBtn = "START"
        }else if(rel.denumirestatus == "CONFIRMED" && rel.relstatus == 1){
          rel.relBtn = "IN PROGRESS"
        }
      }
    })
    this.viewDetailsRelease.sort(function (a,b) {
      return new Date(b.datarelease).getTime() - new Date(a.datarelease).getTime();
    })
    this.dataSource.data = this.viewDetailsRelease as Detaliirelease[];

  }

  formatData(data: string): string {
    return moment(data).format("yyyy-MM-DD hh:mm:ss A")
  }

  formatDurata(durata: string): string {
    return moment(durata).format("HH:mm:ss")
  }

  openDialogWkf(statusName: string, mediu: string, idRelease: number){
      if(this.isAdmin){
        if(statusName == "INITIATE"){
          this.statusNames = [];
          this.statusNames.push("CONFIRMED", "CANCELLED")
        }else if(statusName == "CONFIRMED"){
          this.statusNames = [];
          this.statusNames.push("CANCELLED")
          if(mediu == "PROD"){
            this.statusNames.push("RELEASED")
          }else{
            this.statusNames.push("DELIVERED")
          }
        }else if(statusName == "DELIVERED"){
          this.statusNames = []
          this.statusNames.push("RELEASED")
        }else{
          this.statusNames = []
          this.statusNames.push("INITIATE","CANCELLED")
        }

        const dialogRef = this.dialog.open(WorkflowComponent, {
          width: '250px',
          data: { statusName: statusName , statusNames: this.statusNames, idRelease: idRelease}
        });
        dialogRef.afterClosed().subscribe(res => {
          this.getAllDetailsReleases(mediu);
        })
    
      }else{
        this.snackBar.open("Doar cei cu rol de administrator pot modifica statusul!", "", {duration: 2000, panelClass:["snack-style-danger"]},)
      }
  }

  onClickReleaseBtn(relBtn: string, release: Detaliirelease){
    const warningPopUp = this.dialog.open(WarningComponent, {
      width: '500px',
      data: {relBtn: relBtn, release: release}
    });

    warningPopUp.afterClosed().subscribe(res => {
      this.getAllDetailsReleases(release.denumiremediu);
    })
  }

  copyChg(idImpulse: string){
    this.clipBoard.copy(idImpulse);
    this.snackBar.open("Id-ul a fost copiat!", "",{duration:1000});
    return false;
  }

  openDetails(release: Detaliirelease){
    const dialogDetails = this.dialog.open(DetailsComponent, {
      width: '1000px',
      data: {release: release, isAdmin: this.isAdmin}
    });

    dialogDetails.afterClosed().subscribe(res => {
      this.getAllDetailsReleases(release.denumiremediu);
    })
  }

  openBrv = async(release: Detaliirelease) =>{
    const appDet = await this.appService.getOneAppByDen(release.denumireaplicatie).toPromise();
    this.isBrv = true;
    this.isTest = false;
    const brvDoc = this.dialog.open(DocumentsComponent, {
    width: '1000px',
    data: {release: release, isBrv: this.isBrv, isTest: this.isTest, appDet:appDet, isAdmin: this.isAdmin}
   })

   brvDoc.afterClosed().subscribe(res => {
    this.getAllDetailsReleases(release.denumiremediu);
   })
  }

  openTest = async (release: Detaliirelease) =>{
    const appDet = await this.appService.getOneAppByDen(release.denumireaplicatie).toPromise();
    this.isBrv = false;
    this.isTest = true;
    const brvDoc = this.dialog.open(DocumentsComponent, {
    width: '1000px',
    data: {release: release, isBrv: this.isBrv, isTest: this.isTest, appDet:appDet, isAdmin: this.isAdmin}
   })

   brvDoc.afterClosed().subscribe(res => {
    this.getAllDetailsReleases(release.denumiremediu);
   })
  }

  exportToExcel(){
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(this.table.nativeElement);
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb,ws, "Releases");
    
    let todayDate = moment(new Date()).format("DD-MM-YYYY");
    XLSX.writeFile(wb, "Releases " + todayDate + ".xlsx");
  }

}
