import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { BankServiceService } from '../Services/bank-service.service';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { SignalServiceService } from '../Services/signal-service.service';

@Component({
  selector: 'app-login',
  imports: [FormsModule, CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  LoginForm!: FormGroup;
  isInValidLogin: boolean = false;
  token!: string;

  constructor(
    private bankService: BankServiceService,
    private signalRService : SignalServiceService,
    private route : Router
  ) {}

  ngOnInit(): void {
    this.LoginForm = new FormGroup({
      userName: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }

  onLogin() {
    if (this.LoginForm.valid) {
        this.bankService.postLogin(this.LoginForm.get('userName')?.value , this.LoginForm.get('password')?.value)
        .subscribe({
          next : (x) => {
            this.token = x['token'];
            localStorage.clear()
            localStorage.setItem('localToken' ,JSON.stringify(this.token))
            sessionStorage.setItem('localIsLoggedIn',JSON.stringify('true'));
            this.bankService.isLoggedIn = true;
            this.signalRService.buildConnection();
            this.signalRService.startConnection();
            this.route.navigate(['/wallet'])
          },
          error : (er) => {
            this.isInValidLogin = true
            setTimeout(() => {
              this.isInValidLogin = false;
            }, 2000);
          }
        });
    } else {
      this.LoginForm.markAllAsTouched();
    }
  }

  onReset(){
    this.LoginForm.reset()
  }
}
