import { Cliente } from "./cliente";
import { ClienteDetalle } from "./cliente-detalle";

export interface ListCliente {
    cliente: Cliente;
    listClientesDetalle: ClienteDetalle[]; 
}