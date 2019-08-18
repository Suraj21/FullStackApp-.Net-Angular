import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';

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

  constructor(private authservice: AuthService) { }

  ngOnInit() {
  }

  register() {
    this.authservice.register(this.model).subscribe(() => {
      console.log('register successull');
    }, error => {
      console.log(error);
    });
  }

  Cancel() {
    this.cancelRegister.emit(false);
    console.log('Cancalled');
  }

}
