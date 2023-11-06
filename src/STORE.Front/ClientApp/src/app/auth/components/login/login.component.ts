import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { User } from '../../interface/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;

  users: User[] =[];

  constructor(
    private fb: FormBuilder,
    private authService: AuthService
  ) {
    this.loginForm = this.fb.group({
      user: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });

    this.listUsers();
  }

  listUsers(){
    this.authService.listUsers().subscribe({
      next: (resp) => {
        this.users = resp;
      }
    });
  }

  login() {

    const loggedUser = this.users.find(u => u.user === this.userGet?.value)!;

    const usuario: User = {
      id: loggedUser.id,
      user: this.userGet?.value,
      password: this.passwordGet?.value,
      admin: loggedUser.admin
    };
    
    this.authService.iniciarSesion(usuario);
  }

  get userGet() {return this.loginForm.get('user');}
  get passwordGet() {return this.loginForm.get('password');}
}
