import { Component, OnInit } from '@angular/core';
import { PeliculaService } from '../../../services/pelicula-service';
import { PeliculaDTO } from '../../../models/dtos/pelicula.dto';
import { CommonModule } from '@angular/common';
import { PeliculaCard } from '../../../shared/pelicula-card/pelicula-card';

@Component({
  selector: 'app-view-peliculas',
  imports: [
    CommonModule,
    PeliculaCard
  ],
  templateUrl: './view-peliculas.html',
  styleUrl: './view-peliculas.css'
})
export class ViewPeliculas implements OnInit{

  title: string = "Sección de Peliculas";
  peliculas: PeliculaDTO[] = [];

  constructor(
    private peliculasService: PeliculaService
  ){}

  ngOnInit(): void {
    this.obtenerPeliculas();

  }


  obtenerPeliculas(){
    this.peliculasService.listarPeliculas().subscribe((data: PeliculaDTO[]) => {
      this.peliculas = data;
      console.log(this.peliculas)
    })
  }

}
