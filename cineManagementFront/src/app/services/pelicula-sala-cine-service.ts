import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PeliculaSalaCineCreateDTO, PeliculaSalaCineDTO, PeliculaSalaCineUpdateDTO } from '../models/dtos/peliculaSalaCine.dto';
import { catchError, forkJoin, map, mergeMap, Observable, of } from 'rxjs';
import { PeliculaService } from './pelicula-service';
import { SalaCineService } from './sala-cine-service';

@Injectable({
  providedIn: 'root'
})
export class PeliculaSalaCineService {
  private apiUrl: string = 'http://localhost:5145/api/PeliculasSalasCine';

  constructor(
    private http: HttpClient,
    private peliculaService: PeliculaService,
    private salaCineService: SalaCineService
  ) {}

  /**
   * Obtener todas las asignaciones
   */
  listarPeliculasSalaCine(): Observable<PeliculaSalaCineDTO[]> {
    return this.http.get<PeliculaSalaCineDTO[]>(this.apiUrl);
  }

  /**
   * Obtener una asignación por ID
   */
  obtenerPorId(id: number): Observable<PeliculaSalaCineDTO> {
    return this.http.get<PeliculaSalaCineDTO>(`${this.apiUrl}/${id}`);
  }

  /**
   * Crear una nueva asignación
   */
  crear(dto: PeliculaSalaCineCreateDTO): Observable<PeliculaSalaCineDTO> {
    return this.http.post<PeliculaSalaCineDTO>(this.apiUrl, dto);
  }

  /**
   * Actualizar una asignación existente
   */
  actualizar(id: number, dto: PeliculaSalaCineUpdateDTO): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, dto);
  }

  /**
   * Eliminar una asignación
   */
  eliminar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  /**
   * Obtener asignaciones por sala
   */
  obtenerPorSala(salaId: number): Observable<PeliculaSalaCineDTO[]> {
    return this.http.get<PeliculaSalaCineDTO[]>(`${this.apiUrl}/por-sala/${salaId}`);
  }

  /**
   * Obtener asignaciones por película
   */
  obtenerPorPelicula(peliculaId: number): Observable<PeliculaSalaCineDTO[]> {
    return this.http.get<PeliculaSalaCineDTO[]>(`${this.apiUrl}/por-pelicula/${peliculaId}`);
  }

  /**
   * Verificar disponibilidad de una sala entre dos fechas
   */
  verificarDisponibilidad(salaId: number, inicio: Date, fin: Date): Observable<boolean> {
    const params = new HttpParams()
      .set('salaId', salaId.toString())
      .set('inicio', inicio.toISOString())
      .set('fin', fin.toISOString());

    return this.http.get<boolean>(`${this.apiUrl}/disponibilidad`, { params });
  }

  listarPeliculasSalaCineDetails(): Observable<PeliculaSalaCineDTO[]> {
    return this.http.get<PeliculaSalaCineDTO[]>(this.apiUrl).pipe(
      mergeMap(programaciones => {

        if (!programaciones || programaciones.length === 0) {
          return of([]);
        }

        // Crear un array de observables para obtener detalles
        const requests = programaciones.map(programacion => {
          const pelicula$ = this.peliculaService.obtenerPeliculaPorId(programacion.peliculaId).pipe(
            catchError(() => of(null)) // Manejar errores devolviendo null
          );

          const sala$ = this.salaCineService.obtenerSalaCinePorId(programacion.salaId).pipe(
            catchError(() => of(null)) // Manejar errores devolviendo null
          );

          return forkJoin([pelicula$, sala$]).pipe(
            map(([pelicula, sala]) => ({
              ...programacion,
              pelicula: pelicula || undefined,
              sala: sala || undefined
            }))
          );
        });

        return forkJoin(requests);
      })
    );
  }

}
