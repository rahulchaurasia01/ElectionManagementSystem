import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AdminService } from 'src/app/services/admin/admin.service';
import { Login } from 'src/app/Model/login';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  userInformation: FormGroup;
  errorMsg: string;

  constructor(private admin: AdminService, private _router: Router) { }

  ngOnInit() {
    this.errorMsg = "";

    if(localStorage.getItem('electionToken')) {
      this._router.navigate(['']);
    }

    this.userInformation = new FormGroup({
      emailId: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(5)])
    })

  }

  hasError(formName: string, errorType: string) {

    if (formName == "emailId") {
      if (this.userInformation.controls[formName].untouched)
        return false;
    }

    if (formName == "password") {
      if (this.userInformation.controls['password'].untouched)
        return false;
    }

    return this.userInformation.controls[formName].hasError(errorType);
  }

  userData(body) {
    if(this.userInformation.valid) {
      this.sendDataToServer(body);
    }
  }

  private sendDataToServer(body) {

    var login: Login = {
      EmailId: body.emailId,
      Password: body.password
    };

    this.admin.login(login).
      subscribe(data => {
        if(data.status) {
          localStorage.removeItem('electionToken');
          localStorage.setItem("electionToken", data.token);
          this.userInformation.reset();
          this._router.navigate(['']);
        }
        else {
          this.errorMsg = data.message;
        }
      },
      error => {
        if(error.error.message) {
          this.errorMsg = error.error.message;
        }
        else {
          this.errorMsg = "Connection to the server failed.";
        }
      })    

  }

}
