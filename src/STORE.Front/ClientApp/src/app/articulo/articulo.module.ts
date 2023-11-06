import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ArticuloRoutingModule } from './articulo.routing';
import { ArticuloComponent } from './articulo.component';
import { SharedModule } from '../shared/shared.module';
import { RegistroModalComponent } from './components/registro-modal/registro-modal.component';
import { CarritoModalComponent } from './components/carrito-modal/carrito-modal.component';


@NgModule({
  declarations: [
    ArticuloComponent,
    RegistroModalComponent,
    CarritoModalComponent
  ],
  imports: [
    CommonModule,
    ArticuloRoutingModule,
    SharedModule
  ]
})
export class ArticuloModule { }
