import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClienteRoutingModule } from './cliente.routing';
import { ClienteComponent } from './cliente.component';
import { RegistroModalComponent } from './components/registro-modal/registro-modal.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    ClienteComponent,
    RegistroModalComponent
  ],
  imports: [
    CommonModule,
    ClienteRoutingModule,
    SharedModule
  ]
})
export class ClienteModule { }
