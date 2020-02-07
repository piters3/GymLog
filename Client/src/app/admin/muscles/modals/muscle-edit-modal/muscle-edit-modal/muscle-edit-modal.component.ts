import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Muscle } from 'src/app/_models/muscle';
import { MusclesService } from 'src/app/admin/muscles/services/muscles.service';

@Component({
  selector: 'app-musle-edit-modal',
  templateUrl: './muscle-edit-modal.component.html',
  styleUrls: ['./muscle-edit-modal.component.css']
})
export class MuscleEditModalComponent implements OnInit {
  form: FormGroup;
  muscle: Muscle;

  constructor(public bsModalRef: BsModalRef, public fb: FormBuilder, public muscleservice: MusclesService) { }

  ngOnInit() {
    this.createForms();
  }

  createForms(): void {
    this.form = this.fb.group({
      name: [this.muscle.name, Validators.required]
    });
  }

  onSubmit(): void {
    const updatedMuscle: Muscle = { ...this.form.value, id: this.muscle.id };

    this.muscleservice.update(updatedMuscle).subscribe(() => {
      this.bsModalRef.hide();
      this.muscleservice.getAll();
    }, error => {
      console.log(error);
    });
  }

}
