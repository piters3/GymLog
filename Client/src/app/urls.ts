import { environment } from 'src/environments/environment';

export class Urls {
    static homeUrl = '/home';
    static loginUrl = environment.apiUrl + 'auth/login';
    static registerUrl = environment.apiUrl + 'auth/register';
}
