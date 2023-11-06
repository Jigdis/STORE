import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/auth/services/auth.service';

@Component({
  selector: 'app-sidebar-menu',
  templateUrl: './sidebar-menu.component.html',
  styleUrls: ['./sidebar-menu.component.css']
})
export class SidebarMenuComponent {
  sidebarVisible: boolean = true;

  items: MenuItem[] = [];
  
  isAdmin: boolean | undefined = false;

  private userServiceSubscription: Subscription | undefined;

  constructor(private auth: AuthService,){
    this.userServiceSubscription = this.auth.currentUser.subscribe(
        currentUser => {
          this.isAdmin = currentUser.usuario?.admin
        }
      )
  }

    ngOnInit() {
        this.items = [
            {
                label: 'Tiendas',
                icon: 'pi pi-shopping-bag',
                routerLink: ['tienda']
            },
            {
                label: 'Artículos',
                icon: 'pi pi-box',
                routerLink: ['articulos']
            },
        ];

        if(this.isAdmin){
            this.items.push({
                label: 'Clientes',
                icon: 'pi pi-users',
                routerLink: ['/clientes']
            })
        }

    }
}
