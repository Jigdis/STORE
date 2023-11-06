import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { ClienteService } from '../../service/cliente.service';
import { ClienteCreate } from '../../interface/cliente-create';
import { Cliente } from '../../interface/cliente';

@Component({
  selector: 'app-registro-modal',
  templateUrl: './registro-modal.component.html',
  styleUrls: ['./registro-modal.component.css']
})
export class RegistroModalComponent {
  clienteForm: FormGroup;
  cliente: Cliente | null = null;;
  isEdit: boolean = false;

  constructor(
    public dialogRef: DynamicDialogRef,
    private dialogConfig: DynamicDialogConfig,
    private fb: FormBuilder,
    private _messageService: MessageService,
    private _clienteService: ClienteService
  ) {
    this.clienteForm = this.fb.group({
      clienteID: null,
      nombre: [null, Validators.required],
      apellidos: [null, Validators.required],
      direccion: [null, Validators.required]
    });
  }

  ngOnInit() {
    this.cliente = this.dialogConfig.data;
    if(this.cliente != null){
      this.isEdit = true;
      this.clienteIDGet?.setValue(this.cliente.clienteID);
      this.nombreGet?.setValue(this.cliente.nombre);
      this.apellidosGet?.setValue(this.cliente.apellidos);
      this.direccionGet?.setValue(this.cliente.direccion);
    }
  }

  closeDialog() {
    this.dialogRef.close();
  }

  guardar(){
    if(!this.isEdit) {
      this.tiendaCreate();
    }
    else {
      this.tiendaUpdate();
    }
  }

  tiendaCreate() {
    this._clienteService.clienteCreate(this.clienteForm.value as ClienteCreate)
      .subscribe({
        next: (resp) => {
          this._messageService.add({ severity: 'success', summary: 'Artículo', detail: `${resp.message}` });
          this.closeDialog();
        },
        error: (error) => {
          this._messageService.add({ severity: 'error', summary: 'Artículo', detail: `${error.message}` });
          this.closeDialog();
  
        }
      })
  }

  tiendaUpdate(){
    this._clienteService.clienteEdit(this.clienteForm.value as ClienteCreate)
      .subscribe({
        next: (resp) => {
          this._messageService.add({ severity: 'success', summary: 'Artículo', detail: `${resp.message}` });
          this.closeDialog();
        },
        error: (error) => {
          this._messageService.add({ severity: 'error', summary: 'Artículo', detail: `${error.message}` });
          this.closeDialog();
  
        }
      })
  }

  get clienteIDGet() {return this.clienteForm.get('clienteID');}
  get nombreGet() {return this.clienteForm.get('nombre');}
  get apellidosGet() {return this.clienteForm.get('apellidos');}
  get direccionGet() {return this.clienteForm.get('direccion');}
}
