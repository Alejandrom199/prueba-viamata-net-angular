using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cineManagementDatabaseFirst.Models.DTOs
{
    public class SalaCineDTO
    {
        public int SalaId { get; set; }
        public string Nombre { get; set; }
        public int? Estado { get; set; }
        public bool? EsActivo { get; set; }
    }

    public class SalaCineCreateDTO
    {
        [Required]
        public string Nombre { get; set; }
        public int? Estado { get; set; }
    }

    public class SalaCineUpdateDTO
    {
        [Required]
        public int SalaId { get; set; }
        [Required]
        public string Nombre { get; set; }
        public int? Estado { get; set; }
    }
}
