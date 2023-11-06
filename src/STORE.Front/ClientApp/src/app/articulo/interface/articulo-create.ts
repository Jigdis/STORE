export interface ArticuloCreate {
    articuloID?: number; 
    codigo: string;
    descripcion: string;
    precio: number;
    imagen: string;
    stock: number;
    tiendaID: number;
}