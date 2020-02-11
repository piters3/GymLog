import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Muscle } from 'src/app/_models/muscle';
import { BsModalRef } from 'ngx-bootstrap';
import { MusclesStore } from '../../services/muscles.store';
import { filter, distinctUntilChanged } from 'rxjs/operators';
import { BaseModalComponent } from 'src/app/_shared/components/base-modal/base-modal.component';

@Component({
  selector: 'app-muscle-add-modal',
  templateUrl: './muscle-add-modal.component.html',
  styleUrls: ['./muscle-add-modal.component.css']
})
export class MuscleAddModalComponent extends BaseModalComponent {
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
      this.bsModalRef.hide();
    });
  }

  createForms(): void {
    this.form = this.fb.group({
      name: ['', Validators.required]
    });
  }

  onSubmit(): void {
    this.musclesStore.add(this.form.value);
  }

}
