import { BrowserModule } from '@angular/platform-browser';
import { LOCALE_ID, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';




import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { TodoComponent } from './todo/todo.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ModalModule } from 'ngx-bootstrap/modal';
import { SearchFlightsComponent } from './search-flights/search-flights.component';
import { AllAngularMaterialsModule } from './angular-material-module';
import { ErrorStateMatcher } from '@angular/material/core';
import { FlightsResolver } from './servicesAndResolvers/FlightsRouteResolver';
import locales from '@angular/common/locales/hr'
import { registerLocaleData } from '@angular/common';
registerLocaleData(locales);

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    TodoComponent,
    SearchFlightsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FontAwesomeModule,
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    ReactiveFormsModule,

    AllAngularMaterialsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'search-flights', component: SearchFlightsComponent ,canActivate: [AuthorizeGuard],resolve:
     {flights:FlightsResolver} },
      { path: 'todo', component: TodoComponent, canActivate: [AuthorizeGuard] },
    ]),
    BrowserAnimationsModule,
    ModalModule.forRoot()
  ], 
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    { provide: ErrorStateMatcher, useClass: SearchFlightsComponent },
    { provide: LOCALE_ID, useValue: 'hr-HR'}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
