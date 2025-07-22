import { PeliculaSalaCine } from "./peliculaSalaCine.entity";

export interface Pelicula {
  peliculaId: number;
  nombre: string;
  descripcion?: string;
  duracion?: number;
  imagen?: string;
  esActivo?: boolean;
  peliculaSalaCines?: PeliculaSalaCine[];
}
