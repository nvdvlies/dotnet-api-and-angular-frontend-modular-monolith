import { FormArray, FormControl, FormGroup } from "@angular/forms";

export class MergeUtil {

  public static mergeIntoFormGroup(updated: any, form: FormGroup): void {
    for (const key in form.controls) {
      const control = form.controls[key] as FormControl | FormGroup | FormArray;
      if (control instanceof FormGroup) {
        this.mergeIntoFormGroup(updated[key], control);
      } else if (control instanceof FormArray) {
        this.mergeFormArray(updated[key], control);
      } else {
        this.mergeFormControl(updated[key], control);
      }
    }
  }

  public static hasMergeConflictInFormGroup(updated: any, pristine: any, form: FormGroup): boolean {
    if (!form.dirty) { 
      // control has not been changed by current user
      return false;
    }
    
    for (const key in form.controls) {
      const control = form.controls[key] as FormControl | FormGroup | FormArray;
      if (control instanceof FormGroup) {
        if (this.hasMergeConflictInFormGroup(updated[key], pristine[key], control)) {
          return true;
        }
      } else if (control instanceof FormArray) {
        if (this.hasMergeConflictInFormArray(updated[key], pristine[key], control)) {
          return true;
        }
      } else {
        if (this.hasMergeConflictInFormControl(updated[key], pristine[key], control)) {
          return true;
        }
      }
    }
    return false;
  }

  private static mergeFormArray(updated: any[], formArray: FormArray): void {
    let i = 0;
    for (const key in formArray.controls) {
      const updatedValue = updated[i];
      const control = formArray.controls[key] as FormControl | FormGroup;
      if (control instanceof FormGroup) {
        this.mergeIntoFormGroup(updatedValue[key], control);
      } else {
        this.mergeFormControl(updatedValue[key], control);
      }
      i++;
    }
  }

  private static mergeFormControl(updated: any, control: FormControl): void {
    if (!control.dirty && updated !== control.value) {
      control.setValue(updated);
      control.markAsPristine();
    }
  }

  private static hasMergeConflictInFormArray(updated: any[], pristine: any[], formArray: FormArray): boolean {
    if (!formArray.dirty) { 
      // control has not been changed by current user
      return false;
    }

    if (updated === pristine) { 
      // value has not been changed by other user
      return false;
    }

    if (updated.length != formArray.length) { 
      // array length is not identical in; merge conflict 
      return true;
    }

    let i = 0;
    for (const key in formArray.controls) {
      const updatedValue = updated[i];
      const pristineValue = updated[i];
      const control = formArray.controls[key] as FormControl | FormGroup;
      if (control instanceof FormGroup) {
        if (this.hasMergeConflictInFormGroup(updatedValue[key], pristineValue[key], control)) {
          return true;
        }
      } else {
        if (this.hasMergeConflictInFormControl(updatedValue[key], pristineValue[key], control)) {
          return true;
        }
      }
      i++;
    }

    return false;
  }

  private static hasMergeConflictInFormControl(updated: any, pristine: any, control: FormControl): boolean {
    if (!control.dirty) { 
      // control has not been changed by current user
      return false;
    }

    if (updated === pristine) { 
      // control value has not been changed by other user
      return false;
    }

    if (updated === control.value) { 
      // both users changed the same control with the same value
      return false;
    }

    // both users changed the same control with different values; merge conflict
    return true;
  }
}