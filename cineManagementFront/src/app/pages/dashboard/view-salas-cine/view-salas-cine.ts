import { Component, OnInit } from '@angular/core';
import { SalaCineDTO } from '../../../models/dtos/salaCine.dto';
import { SalaCineService } from '../../../services/sala-cine-service';
import { SalaCard } from "../../../shared/sala-card/sala-card";

@Component({
  selector: 'app-view-salas-cine',
  imports: [SalaCard],
  templateUrl: './view-salas-cine.html',
  styleUrl: './view-salas-cine.css'
})
export class ViewSalasCine implements OnInit{

  title: string = "Sección Salas de Cine";
  salas: SalaCineDTO[] = [];

  constructor(
    private salaCineService: SalaCineService
  ){}

  ngOnInit(): void {
    this.obtenerPeliculas();

  }


  obtenerPeliculas(){
    this.salaCineService.listarSalasCine().subscribe((data: SalaCineDTO[]) => {
      this.salas = data;
      console.log(this.salas)
    })
  }

}
