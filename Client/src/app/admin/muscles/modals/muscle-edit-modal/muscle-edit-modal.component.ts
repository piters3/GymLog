import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Muscle } from 'src/app/_models/muscle';
import { MusclesStore } from '../../services/muscles.store';
import { Observable } from 'rxjs';
import { filter, distinctUntilChanged, takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-musle-edit-modal',
  templateUrl: './muscle-edit-modal.component.html',
  styleUrls: ['./muscle-edit-modal.component.css']
})
export class MuscleEditModalComponent implements OnInit {
  form: FormGroup;
  muscle: Muscle;
  success$: Observable<boolean>;

  constructor(public bsModalRef: BsModalRef, public fb: FormBuilder, public musclesStore: MusclesStore) { }

  ngOnInit() {
    this.createForms();
    this.success$ = this.musclesStore.success$;

    this.success$.pipe(
      filter(x => x),
      distinctUntilChanged()
    ).subscribe(() => {
      this.musclesStore.resetMuscleState();
      this.bsModalRef.hide();
    });
  }

  createForms(): void {
    this.form = this.fb.group({
      name: [this.muscle.name, Validators.required]
    });
  }

  onSubmit(): void {
    const updatedMuscle: Muscle = { ...this.form.value, id: this.muscle.id };
    this.musclesStore.update(updatedMuscle);
  }

}
