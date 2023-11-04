import { NgModule } from '@angular/core';
import { SidebarModule } from 'primeng/sidebar';
import { ButtonModule } from 'primeng/button';
import { MenuModule } from 'primeng/menu';

@NgModule({
  exports: [
    SidebarModule,
    ButtonModule,
    MenuModule
  ]
})
export class PrimeNgModule { }
