import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AppComponent } from '../app.component'
import { AlertService, AuthenticationService } from '../_services/index';
import { UserDetails } from '../_modals/UserDetails';
import { Observable } from 'rxjs';
import { flattenStyles } from '@angular/platform-browser/src/dom/dom_renderer';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model: any = {};
  returnurl: string;
  loading = false;
  isInvalidLogin: boolean = false;
  loggedInUserInfo: any =[];
  loggedInUserDetails: UserDetails;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private alertService: AlertService,
    private appComponent: AppComponent
  ) { }

  ngOnInit() {
    this.authenticationService.logout();
    this.returnurl = this.route.snapshot.queryParams['returnUrl'] || '/inventory';
  }

  login() {
    this.loading = true;
    this.authenticationService.login(this.model.username, this.model.password)
      .subscribe(
        (data : any) => {
           localStorage.setItem('userToken',data.access_token)
           this.appComponent.isUserLoggedIn = this.appComponent.IsUserLoggedIn();
           if(this.appComponent.isUserLoggedIn){
              this.GetUserInfo();
           }
           this.isInvalidLogin = false;
           this.router.navigate([this.returnurl]);
        },
      error => {
        this.isInvalidLogin = true;
        this.alertService.error(error);
        this.loading = false;
      });
  }

  GetUserInfo(): void {
    console.log("GetUserInfo");
    this.authenticationService.getLoggedInUserDetails().subscribe((data: {}) => {
      console.log("data " + data);
      this.loggedInUserInfo = data;
      this.loggedInUserDetails = this.loggedInUserInfo;
      console.log(this.loggedInUserDetails);
      localStorage.setItem('UserZId', this.loggedInUserDetails.AccountId)
      localStorage.setItem('UserDisplayName', this.loggedInUserDetails.DisplayName)
    })
  }
}
