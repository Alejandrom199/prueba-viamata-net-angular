import { Component, Input } from '@angular/core';
import { SalaCineDTO } from '../../models/dtos/salaCine.dto';
import { PeliculaDTO } from '../../models/dtos/pelicula.dto';
import { PeliculaSalaCineDTO } from '../../models/dtos/peliculaSalaCine.dto';
import { TruncatePipe } from '../../pipes/truncate.pipe';
import { EstadoSalaPipe } from '../../pipes/estado-sala.pipe';
import { CommonModule, DatePipe } from '@angular/common';

@Component({
  selector: 'app-pelicula-sala-card',
  imports: [
    CommonModule,
    TruncatePipe,
    EstadoSalaPipe,
    DatePipe
  ],
  templateUrl: './pelicula-sala-card.html',
  styleUrl: './pelicula-sala-card.css',
  standalone:true
})
export class PeliculaSalaCard {
  @Input() programacion!: PeliculaSalaCineDTO;

  get pelicula(): PeliculaDTO | undefined {
    return this.programacion.pelicula;
  }

  get sala(): SalaCineDTO | undefined {
    return this.programacion.sala;
  }

  get duracionCartelera(): string {
    const inicio = new Date(this.programacion.fechaPublicacion);
    const fin = new Date(this.programacion.fechaFin);
    const diff = fin.getTime() - inicio.getTime();
    const dias = Math.floor(diff / (1000 * 60 * 60 * 24));

    if (dias > 30) {
      const meses = Math.floor(dias / 30);
      return `${meses} ${meses === 1 ? 'mes' : 'meses'}`;
    }

    return dias > 0 ? `${dias} ${dias === 1 ? 'día' : 'días'}` : 'Menos de 1 día';
  }
}
