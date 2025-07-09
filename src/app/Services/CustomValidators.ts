import { ValidatorFn, AbstractControl, ValidationErrors } from "@angular/forms";

export function passwordMatchValidator(): ValidatorFn {
    return (formGroup: AbstractControl): ValidationErrors | null => {
      const passwordControl = formGroup.get('password'); // Replace 'password' with your actual control name
      const confirmPasswordControl = formGroup.get('confirmPassword'); // Replace 'confirmPassword' with your actual control name
  
      if (!passwordControl || !confirmPasswordControl) {
        return null; // Controls not found, no validation
      }
  
      if (confirmPasswordControl.errors && !confirmPasswordControl.errors['passwordMismatch']) {
        // If confirmPassword already has other errors, don't override them
        return null;
      }
  
      if (passwordControl.value !== confirmPasswordControl.value) {
        confirmPasswordControl.setErrors({ passwordMismatch: true });
        return { passwordMismatch: true }; // Indicate the error at the form group level
      } else {
        confirmPasswordControl.setErrors(null); // Clear any previous mismatch error
        return null;
      }
    };
  }