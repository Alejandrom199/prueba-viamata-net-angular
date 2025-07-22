import { Pelicula } from "./pelicula.entity";
import { SalaCine } from "./salaCine.entity";

export interface PeliculaSalaCine {
  peliculaSalaCineId: number;
  peliculaId: number;
  salaId: number;
  fechaPublicacion: Date;
  fechaFin: Date;
  pelicula?: Pelicula;
  sala?: SalaCine;
}
