using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cineManagementDatabaseFirst.Models.DTOs
{
    public class DashboardDTO
    {
        public int TotalSalas { get; set; }
        public int TotalSalasDisponibles { get; set; }
        public int TotalPeliculas { get; set; }
    }
}
