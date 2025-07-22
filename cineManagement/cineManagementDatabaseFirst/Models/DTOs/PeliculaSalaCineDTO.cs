using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cineManagementDatabaseFirst.Models.DTOs
{
    public class PeliculaSalaCineDTO
    {
        public int PeliculaSalaCineId { get; set; }
        public int PeliculaId { get; set; }
        public int SalaId { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public DateTime FechaFin { get; set; }
        public PeliculaDTO Pelicula { get; set; }
        public SalaCineDTO Sala { get; set; }
    }

    public class PeliculaSalaCineCreateDTO
    {
        [Required]
        public int PeliculaId { get; set; }
        [Required]
        public int SalaId { get; set; }
        [Required]
        public DateTime FechaPublicacion { get; set; }
        [Required]
        public DateTime FechaFin { get; set; }
    }

    public class PeliculaSalaCineUpdateDTO
    {
        [Required]
        public int PeliculaSalaCineId { get; set; }

        [Required]
        public int PeliculaId { get; set; }

        [Required]
        public int SalaId { get; set; }

        [Required]
        public DateTime FechaPublicacion { get; set; }

        [Required]
        public DateTime FechaFin { get; set; }
    }
}
