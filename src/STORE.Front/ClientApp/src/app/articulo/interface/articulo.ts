export interface Articulo {
    articuloTiendaID: number;
    articuloID: number;
    codigo: string;
    descripcion: string;
    precio: number;
    imagen: string;
    stock: number;
    tiendaID: number;
    sucursal: string;
    direccion: string; 
    fecha: Date;
    cantidadCarrito: number;
}