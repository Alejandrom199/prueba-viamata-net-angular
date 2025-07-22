using System;
using System.Collections.Generic;

namespace cineManagementDatabaseFirst.Models.Entities;

public partial class pelicula_sala_cine
{
    public int PeliculaSalaCineId { get; set; }

    public int PeliculaId { get; set; }

    public int SalaId { get; set; }

    public DateTime FechaPublicacion { get; set; }

    public DateTime FechaFin { get; set; }

    public virtual pelicula Pelicula { get; set; } = null!;

    public virtual sala_cine Sala { get; set; } = null!;
}
