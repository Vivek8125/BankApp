import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BankServiceService } from '../Services/bank-service.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-wallet',
  imports: [RouterOutlet, ReactiveFormsModule, FormsModule, CommonModule],
  templateUrl: './wallet.component.html',
  styleUrl: './wallet.component.css',
})
export class WalletComponent implements OnInit {
  hasWallet !: boolean;
  walletBalance: number = 0;
  AccountBalance: number = 0;

  constructor(private bankService: BankServiceService ) {}

  ngOnInit(): void {
    this.bankService.getAccountBalance().subscribe({
      next : (x) =>{
        this.AccountBalance = x['bank'].balance
      },
      error : (er) =>{
        console.log(er)
      }
    });

    this.bankService.getWalletDetails().subscribe({
      next : (x) => {
        if(x['wallet'] == null){
          this.hasWallet = false
        }
        else{
          this.hasWallet = true;
          this.walletBalance = x['wallet'].balance
        }
      },
      error : (er) => {
        console.log(er)
      }

    })
  }


}
