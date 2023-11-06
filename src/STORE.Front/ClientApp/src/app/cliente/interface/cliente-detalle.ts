export interface ClienteDetalle {
    clienteArticuloID: number;
    clienteID: number;
    articuloID: number;
    codigo: string;
    descripcion: string;
    precio: number;
    imagen: string;
    stock: number;
    fecha: Date;
}