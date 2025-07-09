import { Component } from '@angular/core';
import { BankServiceService } from '../Services/bank-service.service';
import { FormsModule } from '@angular/forms';
import { CommonModule, DatePipe, DecimalPipe } from '@angular/common';
import { IGetTransaction } from '../Model/IGetTransaction';
import { TransactinFilterPipe } from '../Pipes/transactin-filter.pipe';

@Component({
  selector: 'app-transaction-history',
  imports: [FormsModule, CommonModule , TransactinFilterPipe , DatePipe],
  templateUrl: './transaction-history.component.html',
  styleUrl: './transaction-history.component.css'
})
export class TransactionHistoryComponent {
  TransactionData : IGetTransaction[] = [];
  filterOption : string = ""

  constructor(private bankService : BankServiceService){
    bankService.getTransaction().subscribe(x => {
      this.TransactionData = x['getTransaction']
    })
  }

}
