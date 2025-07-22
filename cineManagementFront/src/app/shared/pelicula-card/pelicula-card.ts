import { Component, Input } from '@angular/core';
import { PeliculaDTO } from '../../models/dtos/pelicula.dto';


@Component({
  selector: 'app-pelicula-card',
  imports: [],
  templateUrl: './pelicula-card.html',
  styleUrl: './pelicula-card.css',
  standalone: true
})
export class PeliculaCard  {
  @Input() pelicula!: PeliculaDTO;

  get duracionFormateada(): string {
    if (!this.pelicula.duracion) return 'Duración no especificada';

    const horas = Math.floor(this.pelicula.duracion / 60);
    const minutos = this.pelicula.duracion % 60;

    return `${horas > 0 ? horas + 'h ' : ''}${minutos}m`;
  }
}
