import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {  FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { BankServiceService } from './Services/bank-service.service';
import { SignalServiceService } from './Services/signal-service.service';
import { ModeService } from './Services/mode.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet , CommonModule , FormsModule , ReactiveFormsModule , RouterLink],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'my-app2';
  UpdateForm !: FormGroup;
  htmlElement = document.documentElement;
  dark : boolean = true;
  currentColor : string = "dark";

  constructor(public router : Router , public bankService : BankServiceService , public signalRService : SignalServiceService ,public modeService : ModeService){
    this.signalRService.buildConnection();
    this.signalRService.startConnection();
    this.signalRService.getNotification();
    this.currentColor = this.modeService.getTheme()!;
    this.modeService.setTheme(this.currentColor);
    this.modeService.applyTheme(this.htmlElement , this.currentColor);
    this.dark = this.currentColor == "dark" ? true : false ;
  }

  

  logout(){
    this.router.navigate(['/login'])
    localStorage.clear()
    sessionStorage.setItem('localIsLoggedIn' , JSON.stringify('false'));
    this.bankService.isLoggedIn = false;
    this.signalRService.NotificationMessage = ""
  }


  onLogin(){
    this.router.navigate(['/login'])
    localStorage.clear();
  }

  onClick(){
    this.signalRService.NotificationMessage = ""
  }

  onProfile(){
    this.router.navigate(['profile'])
  }

  doLightMode(){
    this.modeService.setTheme('light');
    this.modeService.applyTheme(this.htmlElement , 'light');
    this.dark = false
  }

  doDarkMode(){
    this.modeService.setTheme('dark');
    this.modeService.applyTheme(this.htmlElement , 'dark');
    this.dark = true
  }


}

