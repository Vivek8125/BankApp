import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const guardGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  if(localStorage.getItem('localToken')){
    return true;
  }
  else{
    router.navigate(['/login']);
    return false
  }
};
