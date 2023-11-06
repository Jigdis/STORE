import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Tienda } from '../interface/tienda';
import { Observable, map, throwError } from 'rxjs';
import { BaseResponse } from 'src/app/shared/interface/base-response';

const apiURL = environment.apiURL;
@Injectable({
  providedIn: 'root'
})
export class TiendaService {
  constructor(
    protected _http: HttpClient,
  ) { }

  listTiendas(): Observable<Tienda[]> {
    return this._http
    .get<BaseResponse<Tienda[]>>(`${apiURL}/api/Tiendas/TiendasGetAll`)
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

  tiendaCreate(tienda: Tienda): Observable<BaseResponse<boolean>>{
    const body = JSON.stringify(tienda);  
    return this._http
    .post<BaseResponse<boolean>>(`${apiURL}/api/Tiendas/TiendaCreate`, tienda)
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

  tiendaUpdate(tienda: Tienda): Observable<BaseResponse<boolean>>{
    const body = JSON.stringify(tienda);  
    return this._http
    .put<BaseResponse<boolean>>(`${apiURL}/api/Tiendas/TiendaUpdate`, tienda)
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

  tiendaDelete(tiendaID: number): Observable<BaseResponse<boolean>>{
    return this._http
    .delete<BaseResponse<boolean>>(`${apiURL}/api/Tiendas/TiendaDelete/${tiendaID}`)
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
