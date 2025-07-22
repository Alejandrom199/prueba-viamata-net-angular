import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'estadoSala'
})
export class EstadoSalaPipe implements PipeTransform {
  transform(estado: number | undefined): string {
    switch(estado) {
      case 0: return 'Inactiva';
      case 1: return 'Disponible';
      case 2: return 'En mantenimiento';
      case 3: return 'Reservada';
      default: return 'Estado desconocido';
    }
  }
}
