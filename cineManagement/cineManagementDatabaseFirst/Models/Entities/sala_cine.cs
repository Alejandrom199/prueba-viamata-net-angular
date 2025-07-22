using System;
using System.Collections.Generic;

namespace cineManagementDatabaseFirst.Models.Entities;

public partial class sala_cine
{
    public int SalaId { get; set; }

    public string Nombre { get; set; } = null!;

    public int? Estado { get; set; }

    public bool? EsActivo { get; set; }

    public virtual ICollection<pelicula_sala_cine> pelicula_sala_cines { get; set; } = new List<pelicula_sala_cine>();
}
