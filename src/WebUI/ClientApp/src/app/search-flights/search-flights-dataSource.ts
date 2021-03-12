import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { BehaviorSubject, Observable,of } from "rxjs";
import {catchError,finalize,map, multicast, share, tap} from 'rxjs/operators'; 
import { AirportClient, IPaginatedListOfSearchFlightsDTO, PaginatedListOfSearchFlightsDTO, SearchFlightsQuery } from "../web-api-client";

export class SearchFlightsDataSource implements DataSource<PaginatedListOfSearchFlightsDTO> {

    private flightsSubject = new BehaviorSubject<PaginatedListOfSearchFlightsDTO[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    public totalCount:number;
    public loading$ = this.loadingSubject.asObservable();

    constructor(private searchFlightsService: AirportClient) {
        this.totalCount=0;
    }

    connect(collectionViewer: CollectionViewer): Observable<PaginatedListOfSearchFlightsDTO[]> {
        console.log("Connecting data source");
        return this.flightsSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        console.log("Disconnecting data source");
        this.flightsSubject.complete();
        this.loadingSubject.complete();
    }

    // loadFlights(courseId: number, filter = '',
    //             sortDirection = 'asc', pageIndex = 0, pageSize = 3) {
        loadFlights(originIATACode='',destinationIATACode='', departureTime:Date,returnDate?:Date,numberOfAdults:number=1,numberOfChildren?:number,numberOfInfants?:number,currencyCode='EUR'
        ,pageNumber=0,pageSize=5,sortOrder='asc',sortColumn='price') {
            var searchQuery= new SearchFlightsQuery();
            searchQuery.originIATACode= originIATACode;
            searchQuery.destinationIATACode= destinationIATACode;
            searchQuery.departureTime=departureTime;
            searchQuery.returnDate=returnDate;
            searchQuery.numberOfAdults=numberOfAdults;
            searchQuery.numberOfChildren=numberOfChildren;
            searchQuery.numberOfInfants=numberOfInfants;
            searchQuery.currencyCode=currencyCode;
            searchQuery.pageNumber=pageNumber;
            searchQuery.pageSize= pageSize;
            searchQuery.sortOrder=sortOrder;
            searchQuery.sortProperty=sortColumn;
            
            

        this.loadingSubject.next(true);
        
        var bs= this.searchFlightsService.searchFlights(searchQuery).pipe(
            share(),
            
        );
        bs.pipe(
            map(res=>res["items"]),
            catchError(() => of([])),
            finalize(() => this.loadingSubject.next(false)))
        .subscribe(records=> this.flightsSubject.next(records));

        bs.pipe(
            map(res=>res.totalCount),
            finalize(() => this.loadingSubject.next(false)))
            .subscribe(top=>this.totalCount=top,()=>this.totalCount=0);
        
        
    }    
}