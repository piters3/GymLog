import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './_guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { DaylogsComponent } from './user/daylogs/daylogs.component';
import { DaylogComponent } from './user/daylog/daylog.component';
import { ExercisesComponent } from './exercises/exercises.component';
import { ExerciseComponent } from './exercise/exercise.component';

// const routes: Routes = [
//   { path: 'home', component: HomeComponent },
//   {
//     path: 'admin', component: AdminPanelComponent, canActivate: [AuthGuard], runGuardsAndResolvers: 'always',
//     data: { roles: ['Admin'] }
//   },
//   { path: '**', redirectTo: 'home', pathMatch: 'full' },
// ];

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'exercises', component: ExercisesComponent },
  { path: 'exercises/:id', component: ExerciseComponent },
  {
    path: 'admin',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: '', component: AdminPanelComponent, data: { roles: ['Admin'] } }
    ]
  },
  {
    path: 'user',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'logs', component: DaylogsComponent, data: { roles: ['User'] } },
      { path: 'logs/:date', component: DaylogComponent, data: { roles: ['User'] } }
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
