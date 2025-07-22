import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { SalaCineDTO } from '../../../models/dtos/salaCine.dto';
import { PeliculaDTO } from '../../../models/dtos/pelicula.dto';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { PeliculaService } from '../../../services/pelicula-service';
import { SalaCineService } from '../../../services/sala-cine-service';
import { MatOption } from '@angular/material/autocomplete';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { PeliculaSalaCineDTO } from '../../../models/dtos/peliculaSalaCine.dto';
import { PeliculaSalaCineService } from '../../../services/pelicula-sala-cine-service';
import { MatSelect } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { FormsModule } from '@angular/forms';
import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';

import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core'; // Si usas opciones de select, aunque MatSelectModule ya incluye opciones

@Component({
  selector: 'app-asignar-pelicula-sala-cine',
imports: [
  CommonModule,
  ReactiveFormsModule,
  FormsModule,
  MatFormFieldModule,
  MatInputModule,
  MatSelectModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatTableModule,
  MatPaginator,
  MatButtonModule,
  MatCheckboxModule
],
  templateUrl: './asignar-pelicula-sala-cine.html',
  styleUrl: './asignar-pelicula-sala-cine.css',
  standalone: true
})
export class AsignarPeliculaSalaCine implements OnInit {
  title: string = 'Asignar Película a Sala';
  form!: FormGroup;
  isEditMode: boolean = false;
  currentId!: number;
  peliculas: PeliculaDTO[] = [];
  salasCine: SalaCineDTO[] = [];
  asignaciones: PeliculaSalaCineDTO[] = [];

  dataSource = new MatTableDataSource<PeliculaSalaCineDTO>();
  displayedColumns: string[] = ['pelicula', 'sala', 'fechaPublicacion','fechaFin', 'acciones'];
  fechaPublicacion: Date | null = null;
  fechaFin: Date | null = null;
  fechaMinima = new Date();

  @ViewChild(MatPaginator) paginador!: MatPaginator;

  constructor(
    private fb: FormBuilder,
    private peliculasService: PeliculaService,
    private salasService: SalaCineService,
    private peliculaSalaCineService: PeliculaSalaCineService,
    private router: Router
  ) {}

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginador;
  }

  ngOnInit(): void {
    this.inicializarForm();

    this.cargarPeliculas();
    this.cargarSalas();
    this.cargarAsignaciones();
  }

  getNombrePelicula(id: number): string {
    const pelicula = this.peliculas.find(p => p.peliculaId === id);
    return pelicula ? pelicula.nombre : 'Desconocida';
  }

  getNombreSala(id: number): string {
    const sala = this.salasCine.find(s => s.salaId === id);
    return sala ? sala.nombre : 'Desconocida';
  }


inicializarForm() {
  this.form = this.fb.group({
    peliculaId: [0, [Validators.required, Validators.min(1)]],
    salaId: [0, [Validators.required, Validators.min(1)]],
    fechaPublicacion: [null, Validators.required],
    fechaFin: [null, Validators.required]
  });
}
  volver(){
    this.router.navigate(['dashboard/salas-cine'])
  }

  cargarPeliculas() {
    this.peliculasService.listarPeliculas().subscribe((data: PeliculaDTO[]) => this.peliculas = data)
  }

  cargarSalas() {
    this.salasService.listarSalasCine().subscribe((data: SalaCineDTO[]) => this.salasCine = data)
  }

  cargarAsignaciones() {
    this.peliculaSalaCineService.listarPeliculasSalaCine().subscribe((data: PeliculaSalaCineDTO[]) => {
      this.dataSource.data = data
      console.log(this.asignaciones)
    })
  }

  onSubmit() {

    if (this.form.invalid) return;

    const formValues = this.form.value;
    const fechaPublicacion = new Date(formValues.fechaPublicacion);
    const fechaFin = new Date(formValues.fechaFin);

    if (fechaFin < fechaPublicacion) {
      alert("La fecha de fin no puede ser anterior a la de inicio.");
      return;
    }

    const asignacionData: PeliculaSalaCineDTO = {
      peliculaSalaCineId: this.isEditMode ? this.currentId : 0,
      peliculaId: Number(formValues.peliculaId),
      salaId: Number(formValues.salaId),
      fechaPublicacion: fechaPublicacion,
      fechaFin: fechaFin
    };

    if (this.isEditMode) {
      this.peliculaSalaCineService.actualizar(this.currentId, asignacionData).subscribe({
        next: () => {
          alert('Asignación actualizada exitosamente');
          this.cargarAsignaciones();
          this.clearForm();
        },
        error: (err) => console.error('Error al actualizar la asignación:', err)
      });
    } else {
      this.peliculaSalaCineService.crear(asignacionData).subscribe({
        next: () => {
          alert('Asignación creada exitosamente');
          this.cargarAsignaciones();
          this.clearForm();
        },
        error: (err) => console.error('Error al crear la asignación:', err)
      });
    }
  }

  clearForm() {
    this.form.reset({
      peliculaId: 0,
      salaId: 0,
      fechaPublicacion: null,
      fechaFin: null
    });
    this.currentId = 0;
    this.isEditMode = false;
  }


  eliminar(asignacion: PeliculaSalaCineDTO) {
    if (confirm('¿Estás seguro que deseas eliminar esta asignación?')) {
      this.peliculaSalaCineService.eliminar(asignacion.peliculaSalaCineId).subscribe({
        next: () => this.cargarAsignaciones(),
        error: err => console.error('Error eliminando asignación:', err)
      });
    }
  }


  editar(asignacion: PeliculaSalaCineDTO) {
    this.isEditMode = true;
    this.currentId = asignacion.peliculaSalaCineId;

    this.form.patchValue({
      peliculaId: asignacion.peliculaId,
      salaId: asignacion.salaId,
      fechaPublicacion: new Date(asignacion.fechaPublicacion),
      fechaFin: new Date(asignacion.fechaFin)
    });
  }

}
