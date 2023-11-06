import { Component } from '@angular/core';
import { DialogService, DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import { TiendaService } from '../../service/tienda.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Tienda } from '../../interface/tienda';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-registro-modal',
  templateUrl: './registro-modal.component.html',
  styleUrls: ['./registro-modal.component.css']
})
export class RegistroModalComponent {
  tiendaForm: FormGroup;
  tienda: Tienda | null = null;
  isEdit: boolean = false;

  constructor(
    public dialogRef: DynamicDialogRef,
    private dialogConfig: DynamicDialogConfig,
    private fb: FormBuilder,
    private _messageService: MessageService,
    private _tiendaService:TiendaService,

  ) {
    this.tiendaForm = this.fb.group({
      tiendaID: null,
      sucursal: [null, [Validators.required]],
      direccion: [null, [Validators.required]],
    });
  }

  ngOnInit() {
    this.tienda = this.dialogConfig.data;
    if(this.tienda != null){
      this.isEdit = true;
      this.tiendaIDGet?.setValue(this.tienda.tiendaID);
      this.sucursalGet?.setValue(this.tienda.sucursal);
      this.direccionGet?.setValue(this.tienda.direccion);
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
    this._tiendaService.tiendaCreate(this.tiendaForm.value as Tienda)
      .subscribe({
        next: (resp) => {
          this._messageService.add({ severity: 'success', summary: 'Tienda', detail: `${resp.message}` });
          this.closeDialog();
        },
        error: (error) => {
          this._messageService.add({ severity: 'error', summary: 'Tienda', detail: `${error.message}` });
          this.closeDialog();
  
        }
      })
  }

  tiendaUpdate(){
    this._tiendaService.tiendaUpdate(this.tiendaForm.value as Tienda)
      .subscribe({
        next: (resp) => {
          this._messageService.add({ severity: 'success', summary: 'Tienda', detail: `${resp.message}` });
          this.closeDialog();
        },
        error: (error) => {
          this._messageService.add({ severity: 'error', summary: 'Tienda', detail: `${error.message}` });
          this.closeDialog();
  
        }
      })
  }


  get tiendaIDGet() { return this.tiendaForm.get('tiendaID')};
  get sucursalGet() { return this.tiendaForm.get('sucursal')};
  get direccionGet() { return this.tiendaForm.get('direccion')};
}
