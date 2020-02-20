import { Component } from '@angular/core';
import { BaseModalComponent } from 'src/app/_shared/components/base-modal/base-modal.component';
import { BsModalRef } from 'ngx-bootstrap';
import { FormBuilder, Validators, FormArray, AbstractControl, FormGroup } from '@angular/forms';
import { DaylogsStore } from '../../daylogs/services/daylogs.store';
import { Exercise } from 'src/app/_models/exercise';
import { WorkoutDto } from 'src/app/_models/WorkoutDto';
import { SetDto } from 'src/app/_models/setDto';

@Component({
  selector: 'app-add-daylog-modal',
  templateUrl: './add-daylog-modal.component.html',
  styleUrls: ['./add-daylog-modal.component.css']
})
export class AddDaylogModalComponent extends BaseModalComponent {
  exercises: Exercise[];
  workout: WorkoutDto;

  get setsFormControls(): AbstractControl[] { return (this.form.get('sets') as FormArray).controls; }

  constructor(protected bsModalRef: BsModalRef, protected fb: FormBuilder, private daylogsStore: DaylogsStore) {
    super(bsModalRef, fb);
  }

  ngOnInit() {
    super.ngOnInit();
    this.createForms();
  }

  createForms(): void {
    this.form = this.fb.group({
      exercise: [null, Validators.required],
      sets: this.fb.array([this.createSet(1)])
    });
  }

  createSet(i: number): FormGroup {
    return this.fb.group({
      number: i,
      reps: null,
      weight: null
    } as SetDto);
  }

  addSet() {
    this.setsFormControls.push(this.createSet(this.setsFormControls.length + 1))
  }

  removeSet() {
    if (this.setsFormControls.length > 1)
      this.setsFormControls.pop();
  }

  onSubmit(): void {
    // this.workout = { ...this.form.getRawValue() };

    this.workout = {
      exerciseId: this.form.value.exercise.id,
      exerciseName: this.form.value.exercise.name,
      sets: this.form.getRawValue().sets
    }

    this.onClose.next(this.workout);
    this.bsModalRef.hide();
  }
}
