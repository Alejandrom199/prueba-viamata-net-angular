using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cineManagementDatabaseFirst.Models.DTOs
{
    public class SalaEstadoDTO
    {
        public int SalaId { get; set; }
        public string Nombre { get; set; }
        public int? Estado { get; set; }
        public int CantidadPeliculas { get; set; }
        public string EstadoSala { get; set; }
        public IEnumerable<PeliculaDTO> Peliculas { get; set; }
    }
}
