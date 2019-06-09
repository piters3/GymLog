import { environment } from 'src/environments/environment';

export class Urls {
    static homeUrl = '/home';
    static loginUrl = environment.apiUrl + 'auth/login';
    static registerUrl = environment.apiUrl + 'auth/register';
    static usersUrl = environment.apiUrl + 'auth/users/';
    static userUrl = environment.apiUrl + 'auth/users';
    static usersWithRolesUrl = environment.apiUrl + 'admin/userswithroles';
    static editUserRolesUrl = environment.apiUrl + 'admin/editroles/';
}
