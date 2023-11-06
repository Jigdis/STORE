import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-sidebar-menu',
  templateUrl: './sidebar-menu.component.html',
  styleUrls: ['./sidebar-menu.component.css']
})
export class SidebarMenuComponent {
  sidebarVisible: boolean = true;

  items: MenuItem[] = [];

    ngOnInit() {
        this.items = [
            {
                label: 'Tiendas',
                icon: 'pi pi-shopping-bag',
                routerLink: ['/menu']
            },
            {
                label: 'Artículos',
                icon: 'pi pi-box',
                routerLink: ['/menu2']
            },
            {
                label: 'Clientes',
                icon: 'pi pi-users',
                routerLink: ['/menu3']
            }
        ];
    }
}
