import { PeliculaDTO } from "./pelicula.dto";

export interface SalaCineDTO {
  salaId: number;
  nombre: string;
  estado?: number;
  esActivo?: boolean;
}

export interface SalaCineCreateDTO {
  nombre: string;
  estado?: number;
}

export interface SalaCineUpdateDTO {
  salaId: number;
  nombre: string;
  estado?: number;
}

export interface SalaEstadoDTO {
  salaId: number;
  nombre: string;
  estado?: number;
  cantidadPeliculas: number;
  estadoSala: string;
  peliculas: PeliculaDTO[];
}

export interface SalaCineDeactivateDTO {
  salaId: number;
  esActivo: boolean;
}
