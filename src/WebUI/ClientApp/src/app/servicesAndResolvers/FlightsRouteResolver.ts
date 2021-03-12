import {ActivatedRouteSnapshot, Resolve, RouterStateSnapshot} from "@angular/router";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import { map, tap} from 'rxjs/operators';
import { AirportClient, IPaginatedListOfSearchFlightsDTO, PaginatedListOfSearchFlightsDTO, SearchFlightsDTO, SearchFlightsQuery } from "../web-api-client";

@Injectable({providedIn:'root'})
export class FlightsResolver implements Resolve<IPaginatedListOfSearchFlightsDTO[]> {

    constructor(private http: HttpClient,private client:AirportClient) {

    }

    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):  Observable<PaginatedListOfSearchFlightsDTO[]> {
        var  sq = new SearchFlightsQuery();
        sq.originIATACode="ZAG";
        sq.destinationIATACode="DBV";
        sq.departureTime= new Date();
        sq.numberOfAdults=1;
        sq.currencyCode="EUR";
        sq.pageNumber=0;
        sq.pageSize=5;
        
        return this.client.searchFlights(sq).pipe(
            tap(()=> console.log("Ucitavam")),
            map(res => res.items));
    }

}