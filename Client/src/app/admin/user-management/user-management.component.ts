import { Component} from '@angular/core';
import { User } from '../../_models/user';
import { BsModalService } from 'ngx-bootstrap';
import { UserEditModalComponent } from './modals/user-edit-modal/user-edit-modal.component';
import { Observable } from 'rxjs';
import { filter, withLatestFrom, takeUntil, distinctUntilChanged } from 'rxjs/operators';
import { Role } from 'src/app/_models/role';
import { BaseComponent } from 'src/app/_shared/components/base/base.component';
import { AdminStore } from './services/admin.store';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent extends BaseComponent {
  users$: Observable<User[]>;
  roles$: Observable<Role[]>;
  user$: Observable<User>;

  constructor(private adminStore: AdminStore, private modalService: BsModalService) {
    super();
  }

  ngOnInit(): void {
    this.loading$ = this.adminStore.loading$;
    this.success$ = this.adminStore.success$;
    this.users$ = this.adminStore.usersWithRoles$;
    this.roles$ = this.adminStore.roles$;
    this.user$ = this.adminStore.user$;

    this.adminStore.loadUsersWithRoles();
    this.adminStore.loadRoles();

    this.success$.pipe(
      filter(x => x)
    ).subscribe(() => {
      this.adminStore.loadUsersWithRoles();
      this.adminStore.resetSuccess();
    });
  }

  onEdit(userId: number) {
    this.adminStore.loadUser(userId);

    this.user$.pipe(
      filter((user: User) => !!user && user.id === userId),
      distinctUntilChanged((prev, curr) => prev.id === curr.id),
      withLatestFrom(this.roles$),
      takeUntil(this.destroy$)
    ).subscribe(([user, roles]) => {
      const initialState = { user, roles };
      this.modalService.show(UserEditModalComponent, { initialState });
    });
  }
}
