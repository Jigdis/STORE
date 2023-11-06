import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, throwError } from 'rxjs';
import { BaseResponse } from 'src/app/shared/interface/base-response';
import { environment } from 'src/environments/environment';
import { ClienteCreate } from '../interface/cliente-create';
import { ListCliente } from '../interface/list-cliente';
import { ClienteCompraArticulo } from '../interface/cliente-compra-articulo';

const apiURL = environment.apiURL;
@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  constructor(
    protected _http: HttpClient,
  ) { }

  listClientes(): Observable<ListCliente[]> {
    return this._http
    .get<BaseResponse<ListCliente[]>>(`${apiURL}/api/Clientes/ListClientes`)
    .pipe(map((response) => {
      if (response.isSuccess) {
        return response.data!;
      }
      else{
        throwError(() => new Error(response.message));
        return [];
      }
    }));
  }

  clienteCreate(cliente: ClienteCreate): Observable<BaseResponse<boolean>>{
    return this._http
    .post<BaseResponse<boolean>>(`${apiURL}/api/Clientes/ClienteCreate`, cliente)
    .pipe(map((response) => {
      if (response.isSuccess) {
        return response;
      }
      else{
        throwError(() => new Error(response.message));
        return response;
      }

    }));
  }

  clienteEdit(cliente: ClienteCreate): Observable<BaseResponse<boolean>>{
    return this._http
    .put<BaseResponse<boolean>>(`${apiURL}/api/Clientes/ClienteEdit`, cliente)
    .pipe(map((response) => {
      if (response.isSuccess) {
        return response;
      }
      else{
        throwError(() => new Error(response.message));
        return response;
      }
    }));
  }

  clienteDelete(clienteID: number): Observable<BaseResponse<boolean>>{
    return this._http
    .delete<BaseResponse<boolean>>(`${apiURL}/api/Clientes/ClienteDelete/${clienteID}`)
    .pipe(map((response) => {
      if (response.isSuccess) {
        return response;
      }
      else{
        throwError(() => new Error(response.message));
        return response;
      }
    }));
  }

  clienteCompraArticulo(clienteCompraArticulo: ClienteCompraArticulo){
    return this._http
    .post<BaseResponse<boolean>>(`${apiURL}/api/Clientes/ClienteCompraArticulo`, clienteCompraArticulo)
    .pipe(map((response) => {
      if (response.isSuccess) {
        return response;
      }
      else{
        throwError(() => new Error(response.message));
        return response;
      }

    }));
  }
  
}