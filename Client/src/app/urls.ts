import { environment } from 'src/environments/environment';

export class Urls {
    static homeUrl = '/home';
    static loginUrl = environment.apiUrl + 'auth/login';
    static registerUrl = environment.apiUrl + 'auth/register';
    static usersUrl = environment.apiUrl + 'users/users';
    static userUrl = environment.apiUrl + 'admin/getuser/';
    static usersWithRolesUrl = environment.apiUrl + 'admin/userswithroles';
    static updateUserUrl = environment.apiUrl + 'admin/updateuser/';
    static rolesUrl = environment.apiUrl + 'admin/roles/';
}
