import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { FormBuilder, Validators } from '@angular/forms';
import { Muscle } from 'src/app/_models/muscle';
import { MusclesStore } from '../../services/muscles.store';
import { filter, distinctUntilChanged } from 'rxjs/operators';
import { BaseModalComponent } from 'src/app/_shared/components/base-modal/base-modal.component';

@Component({
  selector: 'app-musle-edit-modal',
  templateUrl: './muscle-edit-modal.component.html',
  styleUrls: ['./muscle-edit-modal.component.css']
})
export class MuscleEditModalComponent extends BaseModalComponent {
  muscle: Muscle;

  constructor(protected bsModalRef: BsModalRef, protected fb: FormBuilder, private musclesStore: MusclesStore) {
    super(bsModalRef, fb);
  }

  ngOnInit() {
    this.createForms();
    this.success$ = this.musclesStore.success$;
    this.modalLoading$ = this.musclesStore.modalLoading$;

    this.success$.pipe(
      filter(x => x),
      distinctUntilChanged()
    ).subscribe(() => {
      this.musclesStore.resetSuccess();
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
