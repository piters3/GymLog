import { Component, OnInit, OnDestroy } from '@angular/core';
import { MusclesService } from 'src/app/admin/muscles/services/muscles.service';
import { Observable, Subject } from 'rxjs';
import { Muscle } from 'src/app/_models/muscle';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { MuscleEditModalComponent } from './modals/muscle-edit-modal/muscle-edit-modal/muscle-edit-modal.component';
import { filter, withLatestFrom, takeUntil, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-muscles',
  templateUrl: './muscles.component.html',
  styleUrls: ['./muscles.component.css']
})
export class MusclesComponent implements OnInit, OnDestroy {
  isLoading$: Observable<boolean>;
  muscles$: Observable<Muscle[]>;
  muscle$: Observable<Muscle>;
  isSussess$: Observable<boolean>;

  private destroy$: Subject<void> = new Subject();

  constructor(private musclesService: MusclesService, private modalService: BsModalService) { }

  ngOnInit() {
    this.isLoading$ = this.musclesService.getLoading;
    this.musclesService.getAll();
    this.muscles$ = this.musclesService.getMuscles;
    this.muscle$ = this.musclesService.getMuscle;
    this.isSussess$ = this.musclesService.getSucces;
    this.isSussess$.pipe(
      filter(x => x)
    ).subscribe(() => {
      this.musclesService.getAll();
      this.musclesService.clearDeleteState();
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  editMuscle(id: number): void {
    this.musclesService.get(id);

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
    this.musclesService.delete(id);
  }

}
