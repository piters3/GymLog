import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private toastr: ToastrService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401)
          return throwError(error.statusText);

        const applicationError = error.headers.get('Application-Error');

        if (applicationError)
          return throwError(applicationError);

        let serverError = error.error.errors;

        if (!serverError)
          serverError = error.error;

        let modalStateErrors = '';

        if (serverError && typeof serverError === 'object') {
          for (const key in serverError) {
            if (serverError[key]) {
              modalStateErrors += serverError[key] + '\n';
            }
          }
        }

        const resultError = modalStateErrors || serverError || 'Server Error';
        this.toastr.error(resultError);

        return throwError(resultError);
      })
    );
  }
}

export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true
};
