import { NgModule } from '@angular/core';
import { SidebarModule } from 'primeng/sidebar';
import { ButtonModule } from 'primeng/button';
import { MenuModule } from 'primeng/menu';
import { InputTextModule } from 'primeng/inputtext';
import { CardModule } from 'primeng/card';

@NgModule({
  exports: [
    SidebarModule,
    ButtonModule,
    MenuModule,
    InputTextModule,
    CardModule
  ]
})
export class PrimeNgModule { }
