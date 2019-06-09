import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './_guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';

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
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'admin', component: AdminPanelComponent, data: { roles: ['Admin'] } },
    ]
  },
  { path: '**', redirectTo: '', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
