import { PeliculaSalaCineService } from './../../../services/pelicula-sala-cine-service';
import { Component, OnInit } from '@angular/core';
import { PeliculaSalaCineDTO } from '../../../models/dtos/peliculaSalaCine.dto';
import { CommonModule } from '@angular/common';
import { PeliculaSalaCard } from "../../../shared/pelicula-sala-card/pelicula-sala-card";

@Component({
  selector: 'app-view-peliculas-salas-cine',
  imports: [
    CommonModule,
    PeliculaSalaCard
],
  templateUrl: './view-peliculas-salas-cine.html',
  styleUrl: './view-peliculas-salas-cine.css'
})
export class ViewPeliculasSalasCine implements OnInit {
  title: string = "Programación de Películas en Salas";
  programaciones: PeliculaSalaCineDTO[] = [];
  isLoading: boolean = true;
  errorMessage: string | null = null;

  constructor(
    private peliculaSalaCineService: PeliculaSalaCineService
  ) {}

  ngOnInit(): void {
    this.obtenerPeliculasSalaCine();
  }

  obtenerPeliculasSalaCine() {
    this.isLoading = true;
    this.errorMessage = null;

    this.peliculaSalaCineService.listarPeliculasSalaCine().subscribe({
      next: (data: PeliculaSalaCineDTO[]) => {
        this.programaciones = data;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error:', error);
        this.errorMessage = 'Error al cargar las programaciones';
        this.isLoading = false;
      }
    });
  }
}
