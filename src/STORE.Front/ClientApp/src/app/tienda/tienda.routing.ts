import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LoginComponent } from '../auth/components/login/login.component';
import { TiendaComponent } from './tienda.component';


const routes: Routes = [
  {
    path: '',
    component: TiendaComponent,
    pathMatch: 'full',
  },
  {
    path: '**',
    redirectTo: '',
  },
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule],
})
export class TiendaRoutingModule { }
