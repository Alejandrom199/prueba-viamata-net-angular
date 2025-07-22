export interface PeliculaConSalasDTO {
  peliculaId: number;
  nombre: string;
  descripcion: string;
  duracion: number | null;
  imagen: string;
  cantidadSalasAsignadas: number;
}
