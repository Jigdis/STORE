import { Component } from '@angular/core';
import { ConfirmEventType, ConfirmationService, MessageService } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ListCliente } from './interface/list-cliente';
import { ClienteService } from './service/cliente.service';
import { Cliente } from './interface/cliente';
import { RegistroModalComponent } from './components/registro-modal/registro-modal.component';
import { ClienteCreate } from './interface/cliente-create';
import { ClienteDetalle } from './interface/cliente-detalle';

@Component({
  selector: 'app-cliente',
  templateUrl: './cliente.component.html',
  styleUrls: ['./cliente.component.css']
})
export class ClienteComponent {
  clientes: ListCliente[] = [];

  constructor(
    private dialogService: DialogService,
    private _clienteService: ClienteService,
    private _messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {
    this.listTiendas();
  }

  listTiendas() {
    this._clienteService.listClientes().subscribe({
      next: (resp) => {
        this.clientes = resp;
        console.log('clientes',this.clientes)
      },
      error: (error) => {
        this._messageService.add({ severity: 'error', summary: 'Tienda', detail: `${error.message}` });
      }
    })
  }

  edit(cliente: Cliente) {
    console.log('edit', cliente)
    const ref: DynamicDialogRef = this.dialogService.open(RegistroModalComponent, {
      header: 'Editar Cliente',
      width: '50%',
      contentStyle: { overflow: 'auto' },
      baseZIndex: 10000,
      data: {cliente, isEdit:true}
    });

    ref.onClose.subscribe((result) => {
      this.listTiendas()
    });
  }

  delete(cliente: Cliente) {
    this.confirmationService.confirm({
      message: `¿Estás seguro de eliminar ${cliente.nombre} ${cliente.apellidos}?`,
      header: 'Eliminar tienda',
      icon: 'pi pi-info-circle',
      accept: () => {
        this._clienteService.clienteDelete(cliente.clienteID!).subscribe({
          next: (resp) => {
            this._messageService.add({ severity: 'success', summary: 'Cliente', detail: `${resp.message}` });
            this.listTiendas();

          },
          error: (error) => {
            this._messageService.add({ severity: 'error', summary: 'Cliente', detail: `${error.message}` });
            this.listTiendas();
          }
        })
      },
      reject: (type: ConfirmEventType) => {
        this.listTiendas();
      }
    });
  }

  ver(cliente: Cliente, listClienteDetalle: ClienteDetalle){
    const ref: DynamicDialogRef = this.dialogService.open(RegistroModalComponent, {
      header: 'Ver Cliente',
      width: '50%',
      contentStyle: { overflow: 'auto' },
      baseZIndex: 10000,
      data: {cliente, listClienteDetalle ,isVer:true},
    });

    ref.onClose.subscribe((result) => {
      this.listTiendas()
    });
  }

  showAddDialog() {
    const ref: DynamicDialogRef = this.dialogService.open(RegistroModalComponent, {
      header: 'Nuevo Cliente',
      width: '50%',
      contentStyle: { overflow: 'auto' },
      baseZIndex: 10000,
    });

    ref.onClose.subscribe((result) => {
      this.listTiendas()
    });
  }
}
