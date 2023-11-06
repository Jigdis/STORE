import { Component } from '@angular/core';
import { Articulo } from '../../interface/articulo';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { FormBuilder } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { ArticuloService } from '../../service/articulo.service';
import { TiendaService } from 'src/app/tienda/service/tienda.service';
import { ClienteService } from 'src/app/cliente/service/cliente.service';
import { ClienteCompraArticulo } from 'src/app/cliente/interface/cliente-compra-articulo';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/auth/services/auth.service';

@Component({
  selector: 'app-carrito-modal',
  templateUrl: './carrito-modal.component.html',
  styleUrls: ['./carrito-modal.component.css']
})
export class CarritoModalComponent {
  articulos: Articulo[] = [];
  precioTotal: number = 0;
  private userServiceSubscription: Subscription | undefined;

  private clientID: number = 0;

  constructor(
    public dialogRef: DynamicDialogRef,
    private dialogConfig: DynamicDialogConfig,
    private fb: FormBuilder,
    private _messageService: MessageService,
    private _clienteService: ClienteService,
    private auth: AuthService,
  ){
    this.userServiceSubscription = this.auth.currentUser.subscribe(
      currentUser => {
        this.clientID = currentUser.usuario?.id!;
      }
    );
  }

  ngOnInit() {
    this.articulos = this.dialogConfig.data;
    if(this.articulos != null){
      this.calcularPrecioTotal();
    }

  

  }


  calcularPrecioTotal(): void {
    let precioTotal = 0;
  
    for (const articulo of this.articulos) {
      precioTotal += articulo.precio * articulo.cantidadCarrito;
    }
  
    this.precioTotal = precioTotal; // Asigna el resultado a una propiedad
  }

  guardar(){

    this.articulos.forEach(articulo => {
      const compra: ClienteCompraArticulo = {
        articuloID: articulo.articuloID,
        clienteID: this.clientID
      }
      this._clienteService.clienteCompraArticulo(compra).subscribe({
        next: (resp) => {
          this._messageService.add({ severity: 'success', summary: 'Artículo', detail: `${resp.message}` });
          this.dialogRef.close();
        },
        error: (error) => {
          this._messageService.add({ severity: 'error', summary: 'Artículo', detail: `${error.message}` });
          this.dialogRef.close();
        }
      })
    });
    
  }
}
