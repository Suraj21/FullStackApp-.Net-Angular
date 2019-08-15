import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';
import { InventoryDetails } from '../_modals/InventoryDetails';

@Injectable({
  providedIn: 'root'
})
export class InventoryService {

  //baseUrl: string = "http://localhost:61313/api";
  baseUrl: string = "http://132.186.120.47:8083/api";

  constructor(private httpClient : HttpClient) { 
  }

  httpOptions = { 
    headers: new HttpHeaders({
      'Content-Type':'application/json'
    })
  }

  private extractData(res: Response) {
    let body = res;
    return body || { };
  }

  getAllInventory() :Observable<any> {
    return this.httpClient.get(this.baseUrl + '/Inventory/GetInventory').pipe(map(this.extractData));
  }

  getUsersInventory(userId : string) :Observable<any> {
    const params = new HttpParams().set('userId', userId);
    return this.httpClient.get(this.baseUrl + '/Inventory/GetUsersInventory', {params: params}).pipe(map(this.extractData));
  }

  searchUser(searchUserVal: string): Observable<any> {
    let params = new HttpParams().set("searchUser", searchUserVal);
    return this.httpClient.get(this.baseUrl + '/Inventory/GetUserList', {params: params}).pipe(map(this.extractData));
  }

  saveInventoryData(inventoryDataList: InventoryDetails[], userId: string) : void {
    const params = new HttpParams().set('userName', userId);

    this.httpClient.post(this.baseUrl + '/Inventory/SaveInventory',inventoryDataList, {params : params})
    .subscribe(
        (val) => {
            console.log("POST call successful value returned in body", val);
        },
        response => {
            console.log("POST call in error", response);
        },
        () => {
            console.log("The POST observable is now completed.");
        });
  }

  deleteInventory(invId: string) : void {
    this.httpClient.delete(this.baseUrl + '/Inventory/DeleteInventory?invId=' + invId)
    .subscribe(
        (val) => {
            console.log("DELETE call successful value returned in body", 
                        val);
        },
        response => {
            console.log("DELETE call in error", response);
        },
        () => {
            console.log("The DELETE observable is now completed.");
        });
  }
}