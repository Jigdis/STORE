import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './shared/components/layout/layout.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'auth',
    pathMatch: 'full',
  },
  {
    path: 'auth',
    children: [
      {
        path: '',
        loadChildren: () =>
          import('./auth/auth.module').then((m) => m.AuthModule),
      },
    ],
  },
  {
    path: 'cliente',
    component: LayoutComponent,
    children: [
      {
        path: 'tienda',
        loadChildren: () =>
          import('./tienda/tienda.module').then((m) => m.TiendaModule),
      },
    ],
  },
  {
    path: 'admin',
    component: LayoutComponent,
    children: [
      
    ],
  },
  {
    path: '**',
    redirectTo: 'auth',
  },
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(routes, {
      useHash: true,
    }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule { }
