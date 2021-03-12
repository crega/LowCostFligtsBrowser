import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { AirportClient, IPaginatedListOfSearchFlightsDTO, SearchFlightsDTO } from '../web-api-client';
import { SearchFlightsDataSource } from './search-flights-dataSource';

import {FormControl, FormGroupDirective, NgForm, Validators,FormGroup, ValidatorFn, AbstractControl, ValidationErrors} from '@angular/forms';
import {ErrorStateMatcher} from '@angular/material/core';
import { parseDate } from 'ngx-bootstrap';
import { MatPaginator } from '@angular/material/paginator';
import { tap } from 'rxjs/operators';
import { ActivatedRoute } from '@angular/router';
import { MatSort } from '@angular/material/sort';
import { merge } from 'rxjs';

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

export class SearchFlightsComponent implements AfterViewInit,OnInit {
  flight:SearchFlightsDTO[];
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
  
  
  
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  matcher = new MyErrorStateMatcher();
  totalCount?: Number;
  constructor(private flightsService:AirportClient,private route: ActivatedRoute) { 

  }

  ngOnInit(): void {

   
    this.searchForm.controls['arrivalDateFormControl'].setValidators(this.validateDates(this.searchForm));
    this.searchForm.controls['arrivalDateFormControl']
    this.dataSource = new SearchFlightsDataSource(this.flightsService);
    this.totalCount=this.dataSource.totalCount;
    //this.dataSource.loadFlights("ZAG","DBV",new Date(new Date().toLocaleString()),null,1,null,null,"EUR",1,5 );
  }

  ngAfterViewInit() {
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);
    // this.paginator.page
    //     .pipe(
    //         tap(() => this.loadFlightsPage()),
            
    //     )
    //     .subscribe();
        merge(this.sort.sortChange, this.paginator.page)
            .pipe(
                tap(() => this.loadFlightsPage())
            )
            .subscribe();


}

loadFlightsPage() {
    this.dataSource.loadFlights(
      this.searchForm.controls['departureIATACodeFormControl'].value,
      this.searchForm.controls['arrivalIATACodeFormControl'].value,
       this.searchForm.controls['departureDateFormControl'].value,
       this.searchForm.controls['arrivalDateFormControl'].value,
      parseInt(this.searchForm.controls['numberOfAdultsFormControl'].value),
      parseInt(this.searchForm.controls['numberOfChildrenFormControl'].value),
      parseInt(this.searchForm.controls['numberOfInfantsFormControl'].value),
      this.searchForm.controls['currencyFormControl'].value,
        this.paginator.pageIndex,
        this.paginator.pageSize,
        this.sort.direction,
        this.sort.active
        );
        console.log(this.totalCount);

}
  validateDates (group:FormGroup):ValidatorFn{

    const departure = group.controls['departureDateFormControl'].value;

    const arrival = group.controls['arrivalDateFormControl'].value;

  
    var resu=departure && arrival && departure  >= arrival ? { dateNotAfter: true } : null;
    group.controls['arrivalDateFormControl'].setErrors({resu});
  
    return null;
  }

  onSubmit():void {
    console.log("per");
    console.log(this.totalCount);
    this.dataSource.loadFlights(this.searchForm.controls['departureIATACodeFormControl'].value,
    this.searchForm.controls['arrivalIATACodeFormControl'].value,
     this.searchForm.controls['departureDateFormControl'].value,
     this.searchForm.controls['arrivalDateFormControl'].value,
    parseInt(this.searchForm.controls['numberOfAdultsFormControl'].value),
    parseInt(this.searchForm.controls['numberOfChildrenFormControl'].value),
    parseInt(this.searchForm.controls['numberOfInfantsFormControl'].value),
    this.searchForm.controls['currencyFormControl'].value,
    1,
     this.paginator.pageSize,
    )
    console.log(this.totalCount);
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
