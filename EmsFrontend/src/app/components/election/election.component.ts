import { Component, OnInit} from '@angular/core';
import { AdminService } from '../../services/admin/admin.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-election',
  templateUrl: './election.component.html',
  styleUrls: ['./election.component.scss']
})
export class ElectionComponent implements OnInit {

  userLoggedIn : Boolean = false;
  userElectionSelected: string;

  constructor(private admin: AdminService, private _router: Router) { }

  ngOnInit() { 

    if(localStorage.getItem('electionToken')) {
      this.userLoggedIn = true;
    }

  }

  userNavigatetoLogin() {
    this._router.navigate(['login']);
  }

  userNavigateToRegister() {
    this._router.navigate(['register'])
  }

  LogAdminOut() {
    localStorage.removeItem('electionToken');
    this.userLoggedIn = false;
  }


}
