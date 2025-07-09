import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { passwordMatchValidator } from '../Services/CustomValidators';
import { CommonModule } from '@angular/common';
import { BankServiceService } from '../Services/bank-service.service';

@Component({
  selector: 'app-profile',
  imports: [ReactiveFormsModule , CommonModule , FormsModule],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  UpdateForm !: FormGroup;

  constructor(private bankService : BankServiceService){}


  ngOnInit(): void {
    this.UpdateForm = new FormGroup({
      "password" : new FormControl('' , Validators.required),
      "confirmPassword" : new FormControl('' , Validators.required)
    }, { validators: passwordMatchValidator() })
  }


  onUpdate(){
    if(this.UpdateForm.get('password')?.value != "" && this.UpdateForm.get('password')?.value == this.UpdateForm.get('confirmPassword')?.value){
      this.UpdateForm.markAllAsTouched();
      this.bankService.updatePassword(this.UpdateForm.get('password')?.value)
      this.UpdateForm.reset();
    }
    else{
      this.UpdateForm.markAllAsTouched()
    }
  }

  onReset(){
    this.UpdateForm.reset();
  }

}
