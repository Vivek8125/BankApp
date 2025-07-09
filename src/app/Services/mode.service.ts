import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ModeService {

  getTheme(){
    return localStorage.getItem("theme");
  }
  setTheme(theme: string){
    localStorage.setItem("theme", theme);
  }
 
  applyTheme(htmlElement: any, color: string){
    htmlElement.setAttribute('data-bs-theme', color);
  }
}
