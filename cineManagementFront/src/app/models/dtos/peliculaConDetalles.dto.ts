export interface PeliculaConDetallesDTO {
  peliculaId: number;
  nombre: string;
  descripcion: string;
  duracion?: number;
  imagen: string;
  salaNombre: string;
  estadoSala?: number;
  fechaPublicacion: Date;
  fechaFin: Date;
}
