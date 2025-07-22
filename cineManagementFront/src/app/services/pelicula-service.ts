import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { PeliculaCreateDTO, PeliculaDTO, PeliculaUpdateDTO } from '../models/dtos/pelicula.dto';
import { PeliculaConDetallesDTO } from '../models/dtos/peliculaConDetalles.dto';
import { PeliculaConSalasDTO } from '../models/dtos/peliculaConSalas.dto';

@Injectable({
  providedIn: 'root'
})
export class PeliculaService {

  private apiUrl: string = "http://localhost:5145/api/peliculas";

  constructor(private http: HttpClient) { }

  /**
   * Obtiene todas las películas
   */
  listarPeliculas(): Observable<PeliculaDTO[]> {
    return this.http.get<PeliculaDTO[]>(this.apiUrl);
  }

  /**
   * Obtiene una película por su ID
   */
  obtenerPeliculaPorId(id: number): Observable<PeliculaDTO> {
    return this.http.get<PeliculaDTO>(`${this.apiUrl}/${id}`);
  }


  /**
   * Crea una nueva película
   */
  crearPelicula(pelicula: PeliculaCreateDTO): Observable<PeliculaDTO> {
    return this.http.post<PeliculaDTO>(this.apiUrl, pelicula);
  }

  /**
   * Actualiza una película existente
   */
  actualizarPelicula(pelicula: PeliculaUpdateDTO): Observable<PeliculaDTO> {
    return this.http.put<PeliculaDTO>(`${this.apiUrl}/${pelicula.peliculaId}`, pelicula);
  }

  /**
   * Desactiva una película
   */
  desactivarPelicula(id: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/desactivar/${id}`, {});
  }

  /**
   * Busca películas por nombre usando el stored procedure
   */
  buscarPeliculaPorNombre(nombre: string): Observable<{peliculas: PeliculaDTO[], peliculasConSalas: PeliculaConSalasDTO[]}> {
    return this.http.get<PeliculaConSalasDTO[]>(`${this.apiUrl}/buscar/${nombre}`).pipe(
      map(peliculasConSalas => ({
        peliculas: peliculasConSalas.map(p => this.convertToPeliculaDTO(p)),
        peliculasConSalas: peliculasConSalas
      }))
    );
  }

  /**
   * Obtiene películas por fecha usando el stored procedure
   */
  obtenerPeliculasPorFecha(fecha: Date): Observable<PeliculaConDetallesDTO[]> {
    // Formatear la fecha a ISO string (YYYY-MM-DD)
    const fechaISO = fecha.toISOString().split('T')[0];
    return this.http.get<PeliculaConDetallesDTO[]>(`${this.apiUrl}/por-fecha/${fechaISO}`);
  }

  /**
   * Obtiene películas activas (implementación opcional)
   */
  obtenerPeliculasActivas(): Observable<PeliculaDTO[]> {
    return this.http.get<PeliculaDTO[]>(`${this.apiUrl}/activas`);
  }

  private convertToPeliculaDTO(pelicula: PeliculaConSalasDTO): PeliculaDTO {
  return {
    peliculaId: pelicula.peliculaId,
    nombre: pelicula.nombre,
    descripcion: pelicula.descripcion,
    duracion: pelicula.duracion || undefined, // Convierte null a undefined
    imagen: pelicula.imagen,
    esActivo: true // Asume que las películas buscadas están activas o añade este campo al DTO si es necesario
  };
}
}
