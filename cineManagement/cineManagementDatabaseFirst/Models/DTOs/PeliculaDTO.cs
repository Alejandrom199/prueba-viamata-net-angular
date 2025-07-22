using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cineManagementDatabaseFirst.Models.DTOs
{
    public class PeliculaDTO
    {
        public int PeliculaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? Duracion { get; set; }
        public string Imagen { get; set; }
        public bool? EsActivo { get; set; }
    }

    public class PeliculaCreateDTO
    {
        [Required]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? Duracion { get; set; }
        public string Imagen { get; set; }
    }

    public class PeliculaUpdateDTO
    {
        [Required]
        public int PeliculaId { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? Duracion { get; set; }
        public string Imagen { get; set; }
    }
    public class PeliculaDeactivateDTO
    {
        [Required]
        public int PeliculaId { get; set; }
        public bool EsActivo { get; set; } // Siempre será false en este caso
    }
}
