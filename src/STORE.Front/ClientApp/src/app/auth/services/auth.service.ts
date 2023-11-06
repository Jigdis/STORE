import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../interface/user';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Sesion } from '../interface/sesion';
import { Router } from '@angular/router';

const apiURL = environment.mockApi;

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private sesionSubject: BehaviorSubject<Sesion> = new BehaviorSubject({} as Sesion);
  public readonly currentUser: Observable<Sesion> = this.sesionSubject.asObservable();

  constructor(
    protected _http: HttpClient,
    private router: Router
  ) {
  }

  listUsers(): Observable<User[]> {
    return this._http
      .get<User[]>(`${apiURL}/User`);
  }

  iniciarSesion(usuario: User) {
    const sesion: Sesion = {
      sesionActiva: true,
      usuario: {
        user: usuario.user,
        password: usuario.password,
        admin: usuario.admin,
        id: usuario.id,
      }
    }

    this.sesionSubject.next(sesion);
    this.router.navigate(['/tienda']);
  }

  cerrarSesion() {
    const sesion: Sesion = {
      sesionActiva: false
    };
    this.sesionSubject.next(sesion);
    this.router.navigate(['/auth']);
  }

  obtenerSesion() {
    return this.sesionSubject.asObservable();
  }
}
