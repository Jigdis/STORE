import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TiendaComponent } from './tienda.component';
import { TiendaRoutingModule } from './tienda.routing';
import { SharedModule } from '../shared/shared.module';
import { RegistroModalComponent } from './components/registro-modal/registro-modal.component';



@NgModule({
  declarations: [
    TiendaComponent,
    RegistroModalComponent
  ],
  imports: [
    CommonModule,
    TiendaRoutingModule,
    SharedModule
  ]
})
export class TiendaModule {
}
