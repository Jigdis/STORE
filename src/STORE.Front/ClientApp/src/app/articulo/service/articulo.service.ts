import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, throwError } from 'rxjs';
import { BaseResponse } from 'src/app/shared/interface/base-response';
import { environment } from 'src/environments/environment';
import { Articulo } from '../interface/articulo';
import { ArticuloCreate } from '../interface/articulo-create';

const apiURL = environment.apiURL;
@Injectable({
  providedIn: 'root'
})
export class ArticuloService {

  constructor(
    protected _http: HttpClient,
  ) { }

  listArticulos(): Observable<Articulo[]> {
    return this._http
    .get<BaseResponse<Articulo[]>>(`${apiURL}/api/Articulos/ListArticulos`)
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

  articuloCreate(articulo: ArticuloCreate): Observable<BaseResponse<boolean>>{
    return this._http
    .post<BaseResponse<boolean>>(`${apiURL}/api/Articulos/ArticuloCreate`, articulo)
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

  articuloEdit(articulo: ArticuloCreate): Observable<BaseResponse<boolean>>{
    return this._http
    .put<BaseResponse<boolean>>(`${apiURL}/api/Articulos/ArticuloEdit`, articulo)
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

  articuloDelete(articuloID: number): Observable<BaseResponse<boolean>>{
    return this._http
    .delete<BaseResponse<boolean>>(`${apiURL}/api/Articulos/ArticuloDelete/${articuloID}`)
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
