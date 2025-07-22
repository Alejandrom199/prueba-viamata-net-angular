import { PeliculaDTO } from "./pelicula.dto";
import { SalaCineDTO } from "./salaCine.dto";

export interface PeliculaSalaCineDTO {
  peliculaSalaCineId: number;
  peliculaId: number;
  salaId: number;
  fechaPublicacion: Date;
  fechaFin: Date;
  pelicula?: PeliculaDTO;
  sala?: SalaCineDTO;
}

export interface PeliculaSalaCineCreateDTO {
  peliculaId: number;
  salaId: number;
  fechaPublicacion: Date;
  fechaFin: Date;
}

export interface PeliculaSalaCineUpdateDTO {
  peliculaSalaCineId: number;
  peliculaId: number;
  salaId: number;
  fechaPublicacion: Date;
  fechaFin: Date;
}
