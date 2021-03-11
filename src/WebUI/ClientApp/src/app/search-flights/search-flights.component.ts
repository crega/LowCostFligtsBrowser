import { Component, OnInit } from '@angular/core';
import { AirportClient } from '../web-api-client';
import { SearchFlightsDataSource } from './search-flights-dataSource';

import {FormControl, FormGroupDirective, NgForm, Validators,FormGroup, ValidatorFn, AbstractControl, ValidationErrors} from '@angular/forms';
import {ErrorStateMatcher} from '@angular/material/core';
import { parseDate } from 'ngx-bootstrap';

/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}



@Component({
  selector: 'app-search-flights',
  templateUrl: './search-flights.component.html',
  styleUrls: ['./search-flights.component.css']
})

export class SearchFlightsComponent implements OnInit {
  
  dataSource :SearchFlightsDataSource;
  displayedColumns=["id","departureAirport","arrivalAirport","dateOfDeparture","dateOfArrival",
  "numberOfItinerariesArrival","numberOfItinerariesDeparture","numberOfAdults","numberOfChildren",
  "numberOfInfants","price","currency"];
  panelOpenState = false;

  searchForm = new FormGroup({
    'departureIATACodeFormControl': new FormControl('', [
      Validators.required,
      Validators.maxLength(3)
    ]),
    'arrivalIATACodeFormControl': new FormControl('', [
      Validators.required,
      Validators.maxLength(3)
      
    ]),
    'numberOfAdultsFormControl': new FormControl('', [
      Validators.required
    ]),
    
    'departureDateFormControl' : new FormControl('',[
      Validators.required
    ]),
    'arrivalDateFormControl' : new FormControl('',
    ),
    'numberOfChildrenFormControl': new FormControl(''),
    'numberOfInfantsFormControl': new FormControl('' ),
    'currencyFormControl': new FormControl('',[
      Validators.required,
      Validators.maxLength(3)
    ] ),
  },);
  
  
  
  
  matcher = new MyErrorStateMatcher();
  constructor(private flightsService:AirportClient) { 

  }

  ngOnInit(): void {
    this.searchForm.controls['arrivalDateFormControl'].setValidators(this.validateDates(this.searchForm));
    this.searchForm.controls['arrivalDateFormControl']
    this.dataSource = new SearchFlightsDataSource(this.flightsService);
    this.dataSource.loadFlights("ZAG","DBV",new Date(),null,1,null,null,"EUR",1,5 );
  }
  validateDates (group:FormGroup):ValidatorFn{
    console.log("group");
    console.log(group.controls);
    
    const departure = group.controls['departureDateFormControl'].value;

    const arrival = group.controls['arrivalDateFormControl'].value;
    console.log(departure);
    console.log(arrival);
  
    var resu=departure && arrival && departure  >= arrival ? { dateNotAfter: true } : null;
    group.controls['arrivalDateFormControl'].setErrors({resu});
  
    return null;
  }

  onSubmit():void {
    console.log("per");
    this.dataSource.loadFlights(this.searchForm.controls['departureIATACodeFormControl'].value,
    this.searchForm.controls['arrivalIATACodeFormControl'].value,
     this.searchForm.controls['departureDateFormControl'].value,
     this.searchForm.controls['arrivalDateFormControl'].value,
    parseInt(this.searchForm.controls['numberOfAdultsFormControl'].value),
    parseInt(this.searchForm.controls['numberOfChildrenFormControl'].value),
    parseInt(this.searchForm.controls['numberOfInfantsFormControl'].value),
    this.searchForm.controls['currencyFormControl'].value
    //pageNumber,
    //pageSize
    )
  ;
  }
  keyPressAlphaNumeric(event){
    var inp = String.fromCharCode(event.keyCode);
    if (/[A-Z]/.test(inp)) {

      return true;
    } else {

      event.preventDefault();
      return false;
    }
  }
  

}
