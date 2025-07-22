import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DashboardDTO } from '../models/dtos/dashboard.dto';
import { SalaCineCreateDTO, SalaCineDTO, SalaCineUpdateDTO, SalaEstadoDTO } from '../models/dtos/salaCine.dto';

@Injectable({
  providedIn: 'root'
})
export class SalaCineService {

  private apiUrl: string = "http://localhost:5145/api/sala_cine";

  constructor(private http: HttpClient) { }

  obtenerDashboardData(): Observable<DashboardDTO>{
    return this.http.get<DashboardDTO>(`${this.apiUrl}/dashboard`);
  }

  listarSalasCine(): Observable<SalaCineDTO[]>{
    return this.http.get<SalaCineDTO[]>(this.apiUrl);
  }

  obtenerSalaCinePorId(id: number): Observable<SalaCineDTO> {
      return this.http.get<SalaCineDTO>(`${this.apiUrl}/${id}`);
  }

  crearSalaCine(salaCine: SalaCineCreateDTO): Observable<SalaCineDTO>{
    return this.http.post<SalaCineDTO>(this.apiUrl, salaCine);
  }

  actualizarSalaCine(salaCine: SalaCineUpdateDTO): Observable<SalaCineDTO>{
    return this.http.put<SalaCineDTO>(`${this.apiUrl}/${salaCine.salaId}`, salaCine);
  }

  desactivarSalaCine(id: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/desactivar/${id}`, {});
  }

  /**
   * Obtiene el estado de una sala por su nombre
   */
  getEstadoByNombre(nombre: string): Observable<SalaEstadoDTO> {
    return this.http.get<SalaEstadoDTO>(`${this.apiUrl}/estado/${nombre}`);
  }


}
