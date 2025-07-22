export interface PeliculaDTO {
  peliculaId: number;
  nombre: string;
  descripcion?: string;
  duracion?: number;
  imagen?: string;
  esActivo?: boolean;
}

export interface PeliculaCreateDTO {
  nombre: string;
  descripcion?: string;
  duracion?: number;
  imagen?: string;
}

export interface PeliculaUpdateDTO {
  peliculaId: number;
  nombre: string;
  descripcion?: string;
  duracion?: number;
  imagen?: string;
}

export interface PeliculaDeactivateDTO {
  peliculaId: number;
  esActivo: boolean;
}
