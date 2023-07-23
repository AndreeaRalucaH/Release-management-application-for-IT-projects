import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { AddAplicatieComponent } from 'app/add-aplicatie/add-aplicatie.component';
import { Aplicatii } from 'app/models/aplicatii';
import { AplicatiiService } from 'app/services/aplicatii.service';

@Component({
  selector: 'app-aplicatii',
  templateUrl: './aplicatii.component.html',
  styleUrls: ['./aplicatii.component.scss']
})
export class AplicatiiComponent implements OnInit {

  viewDetailsApps: Aplicatii[] = [];
  displayedColumns: string [] = ["denumire", "emails","ownerproiect","managerproiect"];
  dataSource = new MatTableDataSource(this.viewDetailsApps);
  constructor(public appService: AplicatiiService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.getApps();

  }

  getApps = async() => {
    this.viewDetailsApps = await this.appService.getAllApps().toPromise();
    this.dataSource.data = this.viewDetailsApps as Aplicatii[];
  }

  adaugaApp(){
    const creareApp = this.dialog.open(AddAplicatieComponent, {
      width: '800px'
    })

    creareApp.afterClosed().subscribe(res => {
      this.getApps();
    })
  }

}
