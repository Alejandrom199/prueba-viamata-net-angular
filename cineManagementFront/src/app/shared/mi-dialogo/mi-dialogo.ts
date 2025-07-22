import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';

export interface DialogData{
  titulo: string;
  contenido: string;
}

@Component({
  selector: 'app-mi-dialogo',
  imports: [MatDialogModule],
  templateUrl: './mi-dialogo.html',
  styleUrl: './mi-dialogo.css'
})
export class MiDialogo {
  constructor(public dialogRef: MatDialogRef<MatDialogModule>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData){
  }

  onAceptar(){
    this.dialogRef.close('Aceptar');
  }

  onCancelar(){
    this.dialogRef.close('Cancelar');
  }
}
