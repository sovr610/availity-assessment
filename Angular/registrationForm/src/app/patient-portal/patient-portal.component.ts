import { Component, OnInit, DoCheck } from '@angular/core';
import { Validators } from '@angular/forms';
import { patient } from '../patient';

@Component({
  selector: 'app-patient-portal',
  templateUrl: './patient-portal.component.html',
  styleUrls: ['./patient-portal.component.css']
})
export class PatientPortalComponent implements OnInit, DoCheck{


  allowSubmit: boolean = false;
  firstName = "";
  lastName = "";
  npiNumber = "";
  businessAddress = "";
  telephone = "";
  email = "";

  errorEmail = '';
  errorNPInumber = '';



  constructor() {

  }

  ngOnInit(): void {
  }

  ngDoCheck() {
    if(this.firstName !== '' && this.businessAddress !== '' && this.email !== '' && this.lastName !== '' && this.npiNumber !== ''){
      this.allowSubmit = true;
    } else {
      this.allowSubmit = false;
    }
  }

  onSubmit() {

  }

  reset() {
    this.firstName = "";
    this.lastName = "";
    this.email = "";
    this.npiNumber = "";
    this.businessAddress = "";
    this.telephone = "";
  }

}
