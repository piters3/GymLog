import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Muscle } from 'src/app/_models/muscle';
import { BsModalService } from 'ngx-bootstrap';
import { MuscleEditModalComponent } from './modals/muscle-edit-modal/muscle-edit-modal.component';
import { filter, takeUntil, distinctUntilChanged } from 'rxjs/operators';
import { MusclesStore } from './services/muscles.store';
import { BaseComponent } from 'src/app/_shared/components/base/base.component';
import { MuscleAddModalComponent } from './modals/muscle-add-modal/muscle-add-modal.component';

@Component({
  selector: 'app-muscles',
  templateUrl: './muscles.component.html',
  styleUrls: ['./muscles.component.css']
})
export class MusclesComponent extends BaseComponent {
  muscles$: Observable<Muscle[]>;
  muscle$: Observable<Muscle>;

  constructor(private musclesStore: MusclesStore, private modalService: BsModalService) {
    super();
  }

  ngOnInit() {
    this.loading$ = this.musclesStore.loading$;
    this.muscles$ = this.musclesStore.muscles$;
    this.muscle$ = this.musclesStore.muscle$;

    this.musclesStore.getAll();
  }

  onEdit(id: number): void {
    this.musclesStore.get(id);

    this.muscle$.pipe(
      filter((muscle: Muscle) => !!muscle && muscle.id === id),
      distinctUntilChanged((prev, curr) => prev.id === curr.id),
      takeUntil(this.destroy$)
    ).subscribe((muscle) => {
      const initialState = { muscle };
      this.modalService.show(MuscleEditModalComponent, { initialState });
    });
  }

  onDelete(id: number): void {
    this.musclesStore.delete(id);
  }

  onAdd(){
    this.modalService.show(MuscleAddModalComponent);
  }
}
