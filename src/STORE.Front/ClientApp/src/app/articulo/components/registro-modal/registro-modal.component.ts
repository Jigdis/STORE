import { Component } from '@angular/core';
import { ArticuloCreate } from '../../interface/articulo-create';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { MessageService } from 'primeng/api';
import { ArticuloService } from '../../service/articulo.service';
import { Tienda } from 'src/app/tienda/interface/tienda';
import { TiendaService } from 'src/app/tienda/service/tienda.service';

@Component({
  selector: 'app-registro-modal',
  templateUrl: './registro-modal.component.html',
  styleUrls: ['./registro-modal.component.css']
})
export class RegistroModalComponent {
  articuloForm: FormGroup;
  articulo: ArticuloCreate | null = null;
  isEdit: boolean = false;

  tiendas: Tienda[] = [];


  constructor(
    public dialogRef: DynamicDialogRef,
    private dialogConfig: DynamicDialogConfig,
    private fb: FormBuilder,
    private _messageService: MessageService,
    private _articuloService:ArticuloService,
    private _tiendaService: TiendaService,
  ) {
    this.articuloForm = this.fb.group({
      articuloID: null,
      codigo: [null, [Validators.required]],
      descripcion: [null, [Validators.required]],
      precio: [null, [Validators.required]],
      imagen: [null, [Validators.required]],
      stock: [null, [Validators.required]],
      tiendaID: [null, [Validators.required]],
    });
  }

  ngOnInit() {
    this.articulo = this.dialogConfig.data;
    if(this.articulo != null){
      this.isEdit = true;
      this.articuloIDGet?.setValue(this.articulo.articuloID);
      this.codigoGet?.setValue(this.articulo.codigo);
      this.descripcionGet?.setValue(this.articulo.descripcion);
      this.precioGet?.setValue(this.articulo.precio);
      this.imagenGet?.setValue(this.articulo.imagen);
      this.stockGet?.setValue(this.articulo.stock);
      this.tiendaIDGet?.setValue(this.articulo.tiendaID);
    }

    this.listTiendas();
  }

  listTiendas() {
    this._tiendaService.listTiendas().subscribe({
      next: (resp) => {
        this.tiendas = resp;
      },
      error: (error) => {
        this._messageService.add({ severity: 'error', summary: 'Artículo', detail: `${error.message}` });
      }
    })
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
    this._articuloService.articuloCreate(this.articuloForm.value as ArticuloCreate)
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
    this._articuloService.articuloEdit(this.articuloForm.value as ArticuloCreate)
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

  get articuloIDGet() { return this.articuloForm.get('articuloID')};
  get codigoGet() { return this.articuloForm.get('codigo')};
  get descripcionGet() { return this.articuloForm.get('descripcion')};
  get precioGet() { return this.articuloForm.get('precio')};
  get imagenGet() { return this.articuloForm.get('imagen')};
  get stockGet() { return this.articuloForm.get('stock')};
  get tiendaIDGet() { return this.articuloForm.get('tiendaID')};
}
