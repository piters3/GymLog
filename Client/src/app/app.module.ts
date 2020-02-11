import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { BsDropdownModule, TabsModule, ModalModule, BsDatepickerModule } from 'ngx-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './nav/services/auth.service';
import { ErrorInterceptorProvider } from './_interceptors/error.interceptor';
import { AuthGuard } from './_guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { HasRoleDirective } from './_directives/hasRole.directive';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { AdminService } from './admin/user-management/services/admin.service';
import { UserEditModalComponent } from './admin/user-management/modals/user-edit-modal/user-edit-modal.component';
import { LoadingSpinnerComponent } from './_shared/components/loading-spinner/loading-spinner.component';
import { MusclesComponent } from './admin/muscles/muscles.component';
import { MuscleEditModalComponent } from './admin/muscles/modals/muscle-edit-modal/muscle-edit-modal.component';
import { BaseComponent } from './_shared/components/base/base.component';
import { MuscleAddModalComponent } from './admin/muscles/modals/muscle-add-modal/muscle-add-modal.component';
import { ModalSpinnerComponent } from './_shared/components/modal-spinner/modal-spinner.component';
import { BaseModalComponent } from './_shared/components/base-modal/base-modal.component';

export function tokenGetter() {
  return localStorage.getItem('token');
}

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavComponent,
    RegisterComponent,
    AdminPanelComponent,
    HasRoleDirective,
    UserManagementComponent,
    BaseModalComponent,
    UserEditModalComponent,
    MuscleEditModalComponent,
    MuscleAddModalComponent,
    LoadingSpinnerComponent,
    ModalSpinnerComponent,
    BaseComponent,
    MusclesComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BsDatepickerModule.forRoot(),
    TabsModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    JwtModule.forRoot({
      config: {
        tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/auth']
      }
    }),
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true
    }),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  providers: [
    AuthService,
    ErrorInterceptorProvider,
    AuthGuard,
    AdminService
  ],
  entryComponents: [
    BaseModalComponent,
    UserEditModalComponent,
    MuscleEditModalComponent,
    MuscleAddModalComponent
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
