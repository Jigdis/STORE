import { Component } from '@angular/core';
import { ConfirmEventType, ConfirmationService, MessageService } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Articulo } from './interface/articulo';
import { ArticuloService } from './service/articulo.service';
import { ArticuloCreate } from './interface/articulo-create';
import { RegistroModalComponent } from './components/registro-modal/registro-modal.component';
import { AuthService } from '../auth/services/auth.service';
import { Subscription } from 'rxjs';
import { CarritoModalComponent } from './components/carrito-modal/carrito-modal.component';

@Component({
  selector: 'app-articulo',
  templateUrl: './articulo.component.html',
  styleUrls: ['./articulo.component.css']
})
export class ArticuloComponent {
  articulos: Articulo[] = [];
  isAdmin: boolean | undefined = false;
  private userServiceSubscription: Subscription | undefined;

  constructor(
    private dialogService: DialogService,
    private _articuloService: ArticuloService,
    private _messageService: MessageService,
    private confirmationService: ConfirmationService,
    private auth: AuthService,
  ) {
    this.userServiceSubscription = this.auth.currentUser.subscribe(
      currentUser => {
        this.isAdmin = currentUser.usuario?.admin
      }
    );
    this.listTiendas();
  }


  listTiendas() {
    this._articuloService.listArticulos().subscribe({
      next: (resp) => {
        this.articulos = resp;

        this.articulos.forEach(articulo => {
          articulo.cantidadCarrito = 0;
        });
        console.log('articulos',this.articulos)
      },
      error: (error) => {
        this._messageService.add({ severity: 'error', summary: 'Tienda', detail: `${error.message}` });
      }
    })
  }

  edit(articulo: ArticuloCreate) {
    console.log('edit', articulo)
    const ref: DynamicDialogRef = this.dialogService.open(RegistroModalComponent, {
      header: 'Editar artículo',
      width: '50%',
      contentStyle: { overflow: 'auto' },
      baseZIndex: 10000,
      data: articulo
    });

    ref.onClose.subscribe((result) => {
      this.listTiendas()
    });
  }

  delete(articulo: Articulo) {
    this.confirmationService.confirm({
      message: `¿Estás seguro de eliminar ${articulo.descripcion}?`,
      header: 'Eliminar tienda',
      icon: 'pi pi-info-circle',
      accept: () => {
        this._articuloService.articuloDelete(articulo.tiendaID!).subscribe({
          next: (resp) => {
            this._messageService.add({ severity: 'success', summary: 'Artículo', detail: `${resp.message}` });
            this.listTiendas();

          },
          error: (error) => {
            this._messageService.add({ severity: 'error', summary: 'Artículo', detail: `${error.message}` });
            this.listTiendas();
          }
        })
      },
      reject: (type: ConfirmEventType) => {
        this.listTiendas();
      }
    });
  }

  showAddDialog() {
    const ref: DynamicDialogRef = this.dialogService.open(RegistroModalComponent, {
      header: 'Nuevo artículo',
      width: '50%',
      contentStyle: { overflow: 'auto' },
      baseZIndex: 10000,
    });

    ref.onClose.subscribe((result) => {
      this.listTiendas()
    });
  }

  agregarCarrito(articulo: Articulo){
    articulo.cantidadCarrito += 1;
  }

  eliminarCarrito(articulo: Articulo){
  if(articulo.cantidadCarrito > 0)
    articulo.cantidadCarrito -= 1;

  }

  verCarrito(){
    const ref: DynamicDialogRef = this.dialogService.open(CarritoModalComponent, {
      header: 'Carrito',
      width: '50%',
      contentStyle: { overflow: 'auto' },
      baseZIndex: 10000,
      data: this.articulos
    });

    ref.onClose.subscribe((result) => {
      this.listTiendas()
    });
  }
}
