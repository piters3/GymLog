import { FormGroup } from '@angular/forms';

export class CustomValidators {

  static passwordMatch(group: FormGroup) {
    return group.get('password').value === group.get('confirmPassword').value ? null : { mismatch: true };
  }
}
