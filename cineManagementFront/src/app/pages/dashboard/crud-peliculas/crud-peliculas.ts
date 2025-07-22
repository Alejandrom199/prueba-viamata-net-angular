import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { PeliculaService } from '../../../services/pelicula-service';
import { PeliculaCreateDTO, PeliculaDeactivateDTO, PeliculaDTO, PeliculaUpdateDTO } from '../../../models/dtos/pelicula.dto';
import { PeliculaConSalasDTO } from '../../../models/dtos/peliculaConSalas.dto';
import { MiDialogo } from '../../../shared/mi-dialogo/mi-dialogo';

@Component({
  selector: 'app-crud-peliculas',
  imports: [
    MatFormField, MatLabel, ReactiveFormsModule, MatInputModule,
    MatCheckboxModule, MatPaginator, MatTableModule, MatButtonModule, CommonModule
  ],
  templateUrl: './crud-peliculas.html',
  styleUrl: './crud-peliculas.css'
})
export class CrudPeliculas implements OnInit{
  title: string = "Gestión de Películas";
  form!: FormGroup;
  isEditMode: boolean = false;
  currentId!: number;
  dataSource = new MatTableDataSource<PeliculaDTO>();
  displayedColumns: string[] = ['nombre', 'descripcion' ,'duracion', 'actions'];
  peliculasConSalas: PeliculaConSalasDTO[] = [];

  @ViewChild(MatPaginator) paginador!: MatPaginator;

  constructor(
    private peliculaService: PeliculaService,
    private fb: FormBuilder,
    private mydialog: MatDialog
  ) {}

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginador;
  }

  ngOnInit(): void {
    this.inicializarForm();
    this.listarPeliculas();
  }

  inicializarForm(){
    this.form = this.fb.group({
      nombrePelicula: ['', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(50),
        Validators.pattern(/^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s\-:]+$/)
      ]],
      descripcion: ['', [
        Validators.required,
        Validators.minLength(10),
        Validators.maxLength(500)
      ]],
      duracion: ['', [
        Validators.required,
        Validators.pattern(/^[0-9]+$/),
        Validators.min(1),
        Validators.max(999)
      ]],
      imagen: ['', [
        Validators.required,
        Validators.pattern(/^(http|https):\/\/[^ "]+$/)
      ]]
    });
  }

  listarPeliculas() {
    this.peliculaService.listarPeliculas().subscribe((data: PeliculaDTO[]) => {
      this.dataSource.data = data;
    });
  }

  search(searchInput: HTMLInputElement) {
    if (searchInput.value) {
      this.peliculaService.buscarPeliculaPorNombre(searchInput.value).subscribe({
        next: ({peliculas, peliculasConSalas}) => {
          this.dataSource.data = peliculas;
          this.peliculasConSalas = peliculasConSalas;
        },
        error: (err) => console.error(err)
      });
    } else {
      this.listarPeliculas();
    }
  }

  onSubmit() {
    if (this.form.invalid) return;

    const peliculaData: PeliculaCreateDTO | PeliculaUpdateDTO = {
      nombre: this.form.value.nombrePelicula,
      descripcion: this.form.value.descripcion,
      duracion: Number(this.form.value.duracion),
      imagen: this.form.value.imagen
    };

    console.log({peliculaData})

    if (this.isEditMode) {
      (peliculaData as PeliculaUpdateDTO).peliculaId = this.currentId;
      this.peliculaService.actualizarPelicula(peliculaData as PeliculaUpdateDTO).subscribe({
        next: () => {
          alert("Película actualizada exitosamente");
          this.listarPeliculas();
          this.clearForm();
        },
        error: (err) => console.error('Error al actualizar:', err)
      });
    } else {
      this.peliculaService.crearPelicula(peliculaData as PeliculaCreateDTO).subscribe({
        next: () => {
          alert("Película creada exitosamente");
          this.listarPeliculas();
          this.clearForm();
        },
        error: (err) => console.error('Error al crear:', err)
      });
    }
  }

  clearForm() {
      this.form.reset({
        nombre: '',
        descripcion: '',
        duracion: '',
        imagen: '',
        esActivo: true
      });
      this.currentId = 0;
      this.isEditMode = false;
  }

  eliminar(pelicula: PeliculaDTO) {
      const dialogRef = this.mydialog.open(MiDialogo, {
        data: {
          titulo: "Eliminación de Película",
          contenido: `¿Estás seguro de eliminar la película "${pelicula.nombre}"?`
        },
      });

      dialogRef.afterClosed().subscribe(result => {
        if (result === 'Aceptar' && pelicula.peliculaId) {
          this.peliculaService.desactivarPelicula(pelicula.peliculaId).subscribe({
            next: () => {
              alert("Película eliminada/desactivada exitosamente");
              this.listarPeliculas();
            },
            error: (err) => console.error('Error al eliminar:', err)
          });
        }
      });
  }

  editar(pelicula: PeliculaDTO) {
      this.isEditMode = true;
      this.currentId = pelicula.peliculaId;
      this.form.patchValue({
        nombrePelicula: pelicula.nombre,
        descripcion: pelicula.descripcion,
        duracion: pelicula.duracion?.toString(),
        imagen: pelicula.imagen,
        esActivo: pelicula.esActivo
      });
  }

  // Método adicional para activar/desactivar
  toggleActivo(pelicula: PeliculaDTO) {
    const accion = pelicula.esActivo ? 'Desactivar' : 'Activar';
    const dialogRef = this.mydialog.open(MiDialogo, {
      data: {
        titulo: `${accion} Película`,
        contenido: `¿Estás seguro de ${accion.toLowerCase()} la película "${pelicula.nombre}"?`
      },
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 'Aceptar' && pelicula.peliculaId) {
        const updateData: PeliculaDeactivateDTO = {
          peliculaId: pelicula.peliculaId,
          esActivo: !pelicula.esActivo
        };

        this.peliculaService.desactivarPelicula(pelicula.peliculaId).subscribe({
          next: () => {
            alert(`Película ${accion.toLowerCase()}da exitosamente`);
            this.listarPeliculas();
          },
          error: (err) => console.error(`Error al ${accion.toLowerCase()}:`, err)
        });
      }
    });
  }
}
