import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SidebarMenuComponent } from './components/sidebar-menu/sidebar-menu.component';
import { PrimeNgModule } from './prime-ng/prime-ng.module';



@NgModule({
  declarations: [
    SidebarMenuComponent
  ],
  imports: [
    CommonModule,
    PrimeNgModule
  ],
  exports: [
    SidebarMenuComponent
  ]
})
export class SharedModule { }
