import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import { BankServiceService } from '../Services/bank-service.service';
import { IEmployee } from '../Model/IEmployee';
import { Router } from '@angular/router';

@Component({
  selector: 'app-send-money',
  imports: [FormsModule , CommonModule , ReactiveFormsModule],
  templateUrl: './send-money.component.html',
  styleUrl: './send-money.component.css'
})
export class SendMoneyComponent {
  sendTo !: number
  transferAmount !: number;
  EmployeeData : IEmployee[] = [];
  

  constructor(private bankService : BankServiceService , private router : Router){
    bankService.getEmployee().subscribe(x => {
      this.EmployeeData = x
    })
  }

  onSendMoney(sendMoneyForm : NgForm){
    this.bankService.sendMoney(this.sendTo , this.transferAmount).subscribe({
      next : (x) => {
        sendMoneyForm.reset()
        this.router.navigate(['/wallet'])
      },
      error : (er) =>{
        console.log(er)
      }
    })
  }

  onReset(sendMoneyForm : NgForm){
    sendMoneyForm.reset()
  }
}
