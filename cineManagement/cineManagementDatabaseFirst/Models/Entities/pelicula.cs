using System;
using System.Collections.Generic;

namespace cineManagementDatabaseFirst.Models.Entities;

public partial class pelicula
{
    public int PeliculaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int? Duracion { get; set; }

    public string? Imagen { get; set; }

    public bool? EsActivo { get; set; }

    public virtual ICollection<pelicula_sala_cine> pelicula_sala_cines { get; set; } = new List<pelicula_sala_cine>();
}
