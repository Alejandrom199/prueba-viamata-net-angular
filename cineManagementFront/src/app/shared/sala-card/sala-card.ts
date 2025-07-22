import { Component, Input } from '@angular/core';
import { PeliculaSalaCineDTO } from '../../models/dtos/peliculaSalaCine.dto';
import { SalaCineDTO } from '../../models/dtos/salaCine.dto';

@Component({
  selector: 'app-sala-card',
  imports: [],
  templateUrl: './sala-card.html',
  styleUrl: './sala-card.css'
})
export class SalaCard {
  @Input() sala!: SalaCineDTO;

  get estadoTexto(): string {
    switch(this.sala.estado) {
      case 0: return 'Inactiva';
      case 1: return 'Disponible';
      case 2: return 'En mantenimiento';
      default: return 'Estado no especificado';
    }
  }

  get estadoClase(): string {
    switch(this.sala.estado) {
      case 0: return 'inactiva';
      case 1: return 'disponible';
      case 2: return 'mantenimiento';
      default: return 'desconocido';
    }
  }
}
