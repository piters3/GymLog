import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs';
import { BsModalRef } from 'ngx-bootstrap';

@Component({
  selector: 'app-base-modal',
  templateUrl: './base-modal.component.html',
  styleUrls: ['./base-modal.component.css']
})
export class BaseModalComponent implements OnInit {
  protected form: FormGroup;
  protected success$: Observable<boolean>;
  protected modalLoading$: Observable<boolean>;

  constructor(protected bsModalRef: BsModalRef, protected fb: FormBuilder) { }

  ngOnInit() {
  }

}
