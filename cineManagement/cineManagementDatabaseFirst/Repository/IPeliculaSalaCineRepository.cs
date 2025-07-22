using cineManagementDatabaseFirst.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cineManagementDatabaseFirst.Repository
{
    public interface IPeliculaSalaCineRepository
    {
        Task<IEnumerable<pelicula_sala_cine>> GetAll();
        Task<pelicula_sala_cine> GetById(int id);
        Task<pelicula_sala_cine> Create(pelicula_sala_cine asignacion);
        Task<pelicula_sala_cine> Update(pelicula_sala_cine asignacion);
        Task<bool> Delete(int id);
        Task<IEnumerable<pelicula_sala_cine>> GetBySalaId(int salaId);
        Task<IEnumerable<pelicula_sala_cine>> GetByPeliculaId(int peliculaId);
        Task<bool> ExistsConflict(int salaId, DateTime fechaInicio, DateTime fechaFin, int? excludeId = null);
    }
}
