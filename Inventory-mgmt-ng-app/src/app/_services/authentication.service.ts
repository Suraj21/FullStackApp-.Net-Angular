import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpParams } from '@angular/common/http';
import { Router } from "@angular/router";
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class AuthenticationService {

  //baseUrl: string = "http://localhost:61313/";
  baseUrl: string = "http://132.186.120.47:8083/";

    constructor(private http: HttpClient, private router: Router) { }


    private extractData(res: Response) {
       
        let body = res;
        return body || { };
      }

    login(username: string, password: string) {
        localStorage.setItem('userId', username)
        var data = "username=" + username + "&password=" + password + "&grant_type=password" ;
        var reqHeader = new HttpHeaders({ 'Content-Type': 'application/x-www-urlencoded', 'No-Auth':'True' });
        return this.http.post(this.baseUrl + 'token', data, { headers: reqHeader });
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        localStorage.removeItem('userId');
        localStorage.removeItem('userToken');
        this.router.navigateByUrl('/login');
    }

    IsLoggedIn() {
        if(localStorage.getItem('userToken') != null) {
          return true;
        } 
        else {
            return false;
        }
      }

    getLoggedInUserDetails() : Observable<any> {
        var userId = localStorage.getItem("userId");
        const params = new HttpParams().set('userId', userId);
        return this.http.get(this.baseUrl+ 'api/Account/GetLoggedInUserDetails', {params: params}).pipe(map(this.extractData));
      }
}