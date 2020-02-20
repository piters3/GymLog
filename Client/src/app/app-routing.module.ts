import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './_guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { DaylogsComponent } from './user/daylogs/daylogs.component';
import { DaylogComponent } from './user/daylog/daylog.component';
import { ExercisesComponent } from './exercises/exercises.component';
import { ExerciseComponent } from './exercise/exercise.component';
import { AddDaylogComponent } from './user/add-daylog/add-daylog.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'exercises', component: ExercisesComponent },
  { path: 'exercises/:id', component: ExerciseComponent },
  { path: 'logs', component: DaylogsComponent, canActivate: [AuthGuard], runGuardsAndResolvers: 'always', data: { roles: ['User'] } },
  { path: 'logs/new/:date', component: AddDaylogComponent, canActivate: [AuthGuard], runGuardsAndResolvers: 'always', data: { roles: ['User'] } },
  { path: 'logs/:date', component: DaylogComponent, canActivate: [AuthGuard], runGuardsAndResolvers: 'always', data: { roles: ['User'] } },
  { path: 'admin', component: AdminPanelComponent, canActivate: [AuthGuard], runGuardsAndResolvers: 'always', data: { roles: ['Admin'] } },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
