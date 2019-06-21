import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { NgxUiLoaderService } from 'ngx-ui-loader';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private authService: AuthService, private spinner: NgxUiLoaderService, private toastr: ToastrService) {
  }

  ngOnInit() {
  }

  register() {
    this.spinner.start();
    this.authService.register(this.model).subscribe(() => {
      this.toastr.success('registration successful');
      this.spinner.stop();
    }, error => {
      this.toastr.error(error);
      this.spinner.stop();
    });
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

}
