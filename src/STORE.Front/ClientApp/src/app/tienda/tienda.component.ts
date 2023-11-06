import { Component } from '@angular/core';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { RegistroModalComponent } from './components/registro-modal/registro-modal.component';
import { TiendaService } from './service/tienda.service';
import { Tienda } from './interface/tienda';
import { ConfirmEventType, ConfirmationService, MessageService } from 'primeng/api';
import { Subscription } from 'rxjs';
import { AuthService } from '../auth/services/auth.service';

@Component({
  selector: 'app-tienda',
  templateUrl: './tienda.component.html',
  styleUrls: ['./tienda.component.css'],
})
export class TiendaComponent {

  tiendas: Tienda[] = [];

  isAdmin: boolean | undefined = false;

  private userServiceSubscription: Subscription | undefined;

  constructor(
    private dialogService: DialogService,
    private _tiendaService: TiendaService,
    private _messageService: MessageService,
    private confirmationService: ConfirmationService,
    private auth: AuthService,
  ) {
    this.userServiceSubscription = this.auth.currentUser.subscribe(
      currentUser => {
        this.isAdmin = currentUser.usuario?.admin
      }
    )

    this.listTiendas();
  }

  listTiendas() {
    this._tiendaService.listTiendas().subscribe({
      next: (resp) => {
        this.tiendas = resp;
      },
      error: (error) => {
        this._messageService.add({ severity: 'error', summary: 'Tienda', detail: `${error.message}` });
      }
    })
  }

  edit(tienda: Tienda) {
    console.log('edit', tienda)
    const ref: DynamicDialogRef = this.dialogService.open(RegistroModalComponent, {
      header: 'Nueva tienda',
      width: '50%',
      contentStyle: { overflow: 'auto' },
      baseZIndex: 10000,
      data: tienda
    });

    ref.onClose.subscribe((result) => {
      this.listTiendas()
    });
  }

  delete(tienda: Tienda) {
    this.confirmationService.confirm({
      message: `¿Estás seguro de eliminar ${tienda.sucursal}?`,
      header: 'Eliminar tienda',
      icon: 'pi pi-info-circle',
      accept: () => {
        this._tiendaService.tiendaDelete(tienda.tiendaID!).subscribe({
          next: (resp) => {
            this._messageService.add({ severity: 'success', summary: 'Tienda', detail: `${resp.message}` });
            this.listTiendas();

          },
          error: (error) => {
            this._messageService.add({ severity: 'error', summary: 'Tienda', detail: `${error.message}` });
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
      header: 'Nueva tienda',
      width: '50%',
      contentStyle: { overflow: 'auto' },
      baseZIndex: 10000,
    });

    ref.onClose.subscribe((result) => {
      this.listTiendas()
    });
  }

}
