import { PeliculaSalaCine } from "./peliculaSalaCine.entity";

export interface SalaCine {
  salaId: number;
  nombre: string;
  estado?: number;
  esActivo?: boolean;
  peliculaSalaCines?: PeliculaSalaCine[];
}
