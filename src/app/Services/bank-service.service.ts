import { IEmployee } from './../Model/IEmployee';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBankAccount } from '../Model/IBankAccount';
import { IWallet } from '../Model/IWallet';
import { ITransaction } from '../Model/ITransaction';
import { IGetTransaction } from '../Model/IGetTransaction';
import { environment } from '../Environments/environment';

@Injectable({
  providedIn: 'root',
})
export class BankServiceService {
  localToken: string = '';
  isLoggedIn : boolean =  JSON.parse(sessionStorage.getItem('localIsLoggedIn')!) == 'true' ? true : false;
  loginUrl: string = `${environment.apiBaseUrl}${environment.apiUrls.login}`;
  walletUrl: string = `${environment.apiBaseUrl}${environment.apiUrls.wallet}`;
  bankAccountUrl: string = `${environment.apiBaseUrl}${environment.apiUrls.bank}`;
  employeeUrl: string = `${environment.apiBaseUrl}${environment.apiUrls.employee}`;
  transactionUrl: string = `${environment.apiBaseUrl}${environment.apiUrls.transaction}`;

  constructor(private http: HttpClient) { }

  postLogin(userName: string, password: string) {
    return this.http.post<{ token: string }>(this.loginUrl, {
      username: userName,
      password: password,
    });
  }
  
  getAccountBalance(){
    this.localToken = JSON.parse(localStorage.getItem('localToken')!);
    return this.http.get<{bank : IBankAccount}>(this.bankAccountUrl , {headers : {"Authorization" :`Bearer ${this.localToken}`}})
  }

  getWalletDetails(){
    this.localToken = JSON.parse(localStorage.getItem('localToken')!);
    return this.http.get<{wallet : IWallet}>(this.walletUrl , {headers : {"Authorization" :`Bearer ${this.localToken}`}})
  }

  putAddFunds(balance : number){
    this.localToken = JSON.parse(localStorage.getItem('localToken')!);
    return this.http.put<{wallet2 : IWallet}>(this.walletUrl , {"balance": balance } , {headers : {"Authorization" :`Bearer ${this.localToken}`}} )
  }

  sendMoney(recivedId : number , amount : number){
    this.localToken = JSON.parse(localStorage.getItem('localToken')!);
    return this.http.post<ITransaction>(this.transactionUrl  , {"receiverId": recivedId, "amount": amount  }, {headers : {"Authorization" :`Bearer ${this.localToken}`}} )
  }

  getEmployee(){
    this.localToken = JSON.parse(localStorage.getItem('localToken')!);
    return this.http.get<IEmployee[]>(this.employeeUrl ,  {headers : {"Authorization" :`Bearer ${this.localToken}`}})
  }

  getTransaction(){
    this.localToken = JSON.parse(localStorage.getItem('localToken')!);
    return this.http.get<{getTransaction : IGetTransaction[]}>(this.transactionUrl , {headers : {"Authorization" :`Bearer ${this.localToken}`}})
  }

  updatePassword(password : string){
    this.localToken = JSON.parse(localStorage.getItem('localToken')!);
    return this.http.put(this.employeeUrl , {password : password} , {headers : {"Authorization" :`Bearer ${this.localToken}`}}).subscribe()
  }

}

