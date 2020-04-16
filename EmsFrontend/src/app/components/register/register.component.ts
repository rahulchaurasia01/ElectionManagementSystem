import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Register } from 'src/app/Model/register';
import { AdminService } from '../../services/admin/admin.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  
  errorMsg: string;
  successMsg: string;
  userInformation: FormGroup;

  constructor(private _router: Router, private admin: AdminService) { }

  ngOnInit() {

    this.errorMsg = "";
    this.successMsg = "";

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

    var register: Register = {
      EmailId: body.emailId,
      Password: body.password
    };

    this.admin.register(register).
      subscribe(data => {
        if(data.status) {
          this.userInformation.reset();
          this.successMsg = data.message;
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
