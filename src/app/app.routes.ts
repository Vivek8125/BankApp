import { Routes } from '@angular/router';
import { LoginComponent } from './Login/Login.component';
import { WalletComponent } from './Wallet/Wallet.component';
import { AddFundComponent } from './Add-Fund/Add-Fund.component';
import { SendMoneyComponent } from './Send-Money/Send-Money.component';
import { TransactionHistoryComponent } from './Transaction-History/Transaction-History.component';
import { guardGuard } from './Auth/guard.guard';
import { ProfileComponent } from './profile/profile.component';

export const routes: Routes = [
    {path : "" , redirectTo : "login", pathMatch : 'full'},
    {path : "login", component : LoginComponent },
    {path : "wallet" , component : WalletComponent , canActivate : [guardGuard] },
    {path : "wallet/add-funds" , component : AddFundComponent ,  canActivate : [guardGuard]},
    {path : "wallet/send-money" , component : SendMoneyComponent , canActivate : [guardGuard]},
    {path : "wallet/transaction-history" , component : TransactionHistoryComponent , canActivate : [guardGuard]},
    {path : "profile" , component : ProfileComponent , canActivate : [guardGuard]},
    {path : "**" , redirectTo : "login" , pathMatch : 'full'}
];
