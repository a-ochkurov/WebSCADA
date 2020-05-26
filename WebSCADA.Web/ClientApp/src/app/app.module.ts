import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import {ReactiveFormsModule} from '@angular/forms';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { RealTimeMonitoringComponent } from './schema/real-time-monitoring/real-time-monitoring.component';
import { LogsComponent } from './logs/logs.component';
import { SchemaEditorComponent } from './schema/schema-editor/schema-editor.component';
import { LoginComponent } from './login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    RealTimeMonitoringComponent,
    SchemaEditorComponent,
    LogsComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'logs', component: LogsComponent },
      { path: 'editor', component: SchemaEditorComponent },
      { path: 'monitoring', component: RealTimeMonitoringComponent },
      { path: 'login', component: LoginComponent },
      { path: '**', redirectTo: '/' }
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
