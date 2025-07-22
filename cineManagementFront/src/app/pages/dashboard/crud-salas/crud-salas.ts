import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { SalaCineCreateDTO, SalaCineDeactivateDTO, SalaCineDTO, SalaCineUpdateDTO } from '../../../models/dtos/salaCine.dto';
import { MatPaginator } from '@angular/material/paginator';
import { SalaCineService } from '../../../services/sala-cine-service';
import { MatDialog } from '@angular/material/dialog';
import { MiDialogo } from '../../../shared/mi-dialogo/mi-dialogo';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { AsignarPeliculaSalaCine } from '../asignar-pelicula-sala-cine/asignar-pelicula-sala-cine';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-crud-salas',
  imports: [
    MatFormField, MatLabel, ReactiveFormsModule, MatInputModule, RouterLink,
    MatCheckboxModule, MatPaginator, MatTableModule, MatButtonModule, CommonModule
  ],
  templateUrl: './crud-salas.html',
  styleUrl: './crud-salas.css'
})
export class CrudSalas implements OnInit{
  title: string = "Gestión de Salas de Cine";
  form!: FormGroup;
  isEditMode: boolean = false;
  currentId!: number;
  dataSource = new MatTableDataSource<SalaCineDTO>();
  displayedColumns: string[] = ['nombre', 'estado', 'actions'];

  @ViewChild(MatPaginator) paginador!: MatPaginator;

  constructor(
    private salaCineService: SalaCineService,
    private fb: FormBuilder,
    private mydialog: MatDialog,
  ) {}

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginador;
  }

  ngOnInit(): void {
    this.inicializarForm();
    this.listarSalasCine();
  }

  inicializarForm(){
    this.form = this.fb.group({
      nombreSalaCine: ['', [
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(50),
        Validators.pattern(/^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]+$/)
      ]],
      estado: [1, [Validators.required, Validators.pattern(/^[012]$/)]]
    })
  }

  listarSalasCine(){
    this.salaCineService.listarSalasCine().subscribe((data: SalaCineDTO[]) => {
      this.dataSource.data = data;
    })
  }

  //No realizado en backend
  /*search(searchInput: HTMLInputElement) {
    if (searchInput.value) {
      this.salaCineService.buscarPeliculaPorNombre(searchInput.value).subscribe({
        next: ({peliculas, peliculasConSalas}) => {
          this.dataSource.data = peliculas;
          this.peliculasConSalas = peliculasConSalas;
        },
        error: (err) => console.error(err)
      });
    } else {
      this.listarPeliculas();
    }
  }*/

  onSubmit() {
    if (this.form.invalid) return;

    const salaCineData: SalaCineCreateDTO | SalaCineUpdateDTO = {
      nombre: this.form.value.nombreSalaCine,
      estado: Number(this.form.value.estado),
    };

    console.log({salaCineData})

    if (this.isEditMode) {
      (salaCineData as SalaCineUpdateDTO).salaId = this.currentId;
      this.salaCineService.actualizarSalaCine(salaCineData as SalaCineUpdateDTO).subscribe({
        next: () => {
          alert("Sala de Cine actualizada exitosamente");
          this.listarSalasCine();
          this.clearForm();
        },
        error: (err) => console.error('Error al actualizar:', err)
      });
    } else {
      this.salaCineService.crearSalaCine(salaCineData as SalaCineCreateDTO).subscribe({
        next: () => {
          alert("Sala de Cine creada exitosamente");
          this.listarSalasCine();
          this.clearForm();
        },
        error: (err) => console.error('Error al crear:', err)
      });
    }
  }

  clearForm() {
      this.form.reset({
        nombreSalaCine: '',
        estado: 1,
      });
      this.currentId = 0;
      this.isEditMode = false;
  }

  eliminar(salaCine: SalaCineDTO) {
    const dialogRef = this.mydialog.open(MiDialogo, {
      data: {
        titulo: "Eliminación de Sala de Cine",
        contenido: `¿Estás seguro de eliminar la Sala de Cine "${salaCine.nombre}"?`
      },
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 'Aceptar' && salaCine.salaId) {
        this.salaCineService.desactivarSalaCine(salaCine.salaId).subscribe({
          next: () => {
            alert("Sala de cine eliminada/desactivada exitosamente");
            this.listarSalasCine();
          },
          error: (err) => console.error('Error al eliminar:', err)
        });
      }
    });
  }

  editar(salaCine: SalaCineDTO) {
    this.isEditMode = true;
    this.currentId = salaCine.salaId;
    this.form.patchValue({
      nombreSalaCine: salaCine.nombre,
      estado: salaCine.estado ?? 1,
    });
  }

  toggleActivo(salaCine: SalaCineDTO) {
    const accion = salaCine.esActivo ? 'Desactivar' : 'Activar';
    const dialogRef = this.mydialog.open(MiDialogo, {
      data: {
        titulo: `${accion} Sala de Cine`,
        contenido: `¿Estás seguro de ${accion.toLowerCase()} la Sala de Cine "${salaCine.nombre}"?`
      },
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result === 'Aceptar' && salaCine.salaId) {
        const updateData: SalaCineDeactivateDTO = {
          salaId: salaCine.salaId,
          esActivo: !salaCine.esActivo
        };

        this.salaCineService.desactivarSalaCine(salaCine.salaId).subscribe({
          next: () => {
            alert(`Sala de Cine ${accion.toLowerCase()}da exitosamente`);
            this.listarSalasCine();
          },
          error: (err) => console.error(`Error al ${accion.toLowerCase()}:`, err)
        });
      }
    });
  }

}
