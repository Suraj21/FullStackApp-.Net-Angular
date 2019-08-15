import { Component } from '@angular/core';
import { AuthenticationService } from '../app/_services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'sys-one-inventory';
  isUserLoggedIn: boolean = this.IsUserLoggedIn();
  loggedInUserName: string = localStorage.getItem('UserDisplayName');

  constructor(private authenticationService: AuthenticationService) {
  }

  Logout(): void {
    this.authenticationService.logout();
    this.isUserLoggedIn = this.IsUserLoggedIn();
    console.log("Is User Logged in: "+ this.isUserLoggedIn);
    //window.location.replace('/login');
  }

  IsUserLoggedIn(): boolean {
    return this.authenticationService.IsLoggedIn();
  }
}
