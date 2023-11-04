import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SidebarMenuComponent } from './components/sidebar-menu/sidebar-menu.component';
import { PrimeNgModule } from './prime-ng/prime-ng.module';
import { LayoutComponent } from './components/layout/layout.component';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    SidebarMenuComponent,
    LayoutComponent
  ],
  imports: [
    CommonModule,
    PrimeNgModule,
    ReactiveFormsModule
  ],
  exports: [
    SidebarMenuComponent,
    ReactiveFormsModule,
    PrimeNgModule
  ]
})
export class SharedModule { }
