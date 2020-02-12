import { environment } from 'src/environments/environment';

export class Urls {
    static homeUrl = '/home';
    static loginUrl = environment.apiUrl + 'auth/login';
    static registerUrl = environment.apiUrl + 'auth/register';

    static usersUrl = environment.apiUrl + 'users/users';

    static musclesUrl = environment.apiUrl + 'muscles/';

    static userDaylogsUrl = environment.apiUrl + 'user/daylogs';

    static userUrl = environment.apiUrl + 'admin/getuser/';
    static rolesUrl = environment.apiUrl + 'admin/roles/';
    static updateUserUrl = environment.apiUrl + 'admin/updateuser/';
    static usersWithRolesUrl = environment.apiUrl + 'admin/userswithroles';
}
