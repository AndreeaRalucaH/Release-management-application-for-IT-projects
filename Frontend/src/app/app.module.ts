import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app.routing';
import { ComponentsModule } from './components/components.module';
import { AppComponent } from './app.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { GoogleLoginProvider, SocialLoginModule } from 'angularx-social-login';
import { LoginComponent } from './login/login.component';
import {MatTableModule} from '@angular/material/table';
import {MatDialogModule} from '@angular/material/dialog';
import {MatDividerModule} from '@angular/material/divider';
import { MaterialModule } from './material-module';
import { WarningComponent } from './warning/warning.component';
import { DetailsComponent } from './details/details.component';
import { DocumentsComponent } from './documents/documents.component';
import { UploadComponent } from './upload/upload.component';
import { SubmitEmailComponent } from './submit-email/submit-email.component';
import {RichTextEditorAllModule } from '@syncfusion/ej2-angular-richtexteditor';
import { AplicatiiComponent } from './aplicatii/aplicatii.component';
import { AddAplicatieComponent } from './add-aplicatie/add-aplicatie.component';


@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ComponentsModule,
    RouterModule,
    AppRoutingModule,
    SocialLoginModule,
    MatTableModule,
    MatDialogModule,
    MatDividerModule,
    MaterialModule,
    RichTextEditorAllModule
  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent,
    LoginComponent,
    WarningComponent,
    DetailsComponent,
    DocumentsComponent,
    UploadComponent,
    SubmitEmailComponent,
    AplicatiiComponent,
    AddAplicatieComponent

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
