import { NgModule } from '@angular/core';
import { SidebarModule } from 'primeng/sidebar';
import { ButtonModule } from 'primeng/button';
import { MenuModule } from 'primeng/menu';
import { InputTextModule } from 'primeng/inputtext';
import { CardModule } from 'primeng/card';
import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { DialogService } from 'primeng/dynamicdialog';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DropdownModule } from 'primeng/dropdown';

@NgModule({
  exports: [
    SidebarModule,
    ButtonModule,
    MenuModule,
    InputTextModule,
    CardModule,
    TableModule,
    DialogModule,
    ToastModule,
    ConfirmDialogModule,
    DropdownModule
  ],
  providers: [
    DialogService, 
    MessageService, 
    ConfirmationService
  ]
})
export class PrimeNgModule { }
