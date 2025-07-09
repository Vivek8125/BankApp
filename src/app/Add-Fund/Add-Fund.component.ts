import { BankServiceService } from '../Services/bank-service.service';
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import { SignalServiceService } from '../Services/signal-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-fund',
  imports: [FormsModule , CommonModule , ReactiveFormsModule],
  templateUrl: './add-fund.component.html',
  styleUrl: './add-fund.component.css'
})
export class AddFundComponent {
  addFundToWallet !: number
  errorMessage: any;

  constructor(public bankService : BankServiceService , public signalRService : SignalServiceService , private router : Router){
    this.signalRService.getNotification();
  }

  onAddFunds(addFundsForm : NgForm){
    this.bankService.putAddFunds(this.addFundToWallet).subscribe({
      next : (x) => {
        this.errorMessage = this.signalRService.NotificationMessage
        addFundsForm.reset();
        this.router.navigate(['/wallet'])
      },
      error : (er) =>{
        console.log(er)
      }
    })
  }

  onReset(addFundsForm : NgForm){
    addFundsForm.reset();
  }
}
