import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TiendaComponent } from './tienda.component';
import { TiendaRoutingModule } from './tienda.routing';



@NgModule({
  declarations: [
    TiendaComponent
  ],
  imports: [
    CommonModule,
    TiendaRoutingModule,
  ]
})
export class TiendaModule {
}
