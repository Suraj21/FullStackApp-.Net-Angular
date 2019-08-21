import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  // getting values from parent(home component) to itself as child component(register component)
  // @Input() valuesFromHome: any;

  // communication from child to parent using output properties
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private authservice: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  register() {
    this.authservice.register(this.model).subscribe(() => {
      this.alertify.success('registration successull');
      console.log('register successull');
    }, error => {
      this.alertify.error(error);
      console.log(error);
    });
  }

  Cancel() {
    this.cancelRegister.emit(false);
    // console.log('Cancalled');
  }

}
