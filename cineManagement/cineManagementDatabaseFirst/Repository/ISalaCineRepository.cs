using cineManagementDatabaseFirst.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cineManagementDatabaseFirst.Repository
{
    public interface ISalaCineRepository
    {
        Task<IEnumerable<sala_cine>> GetAll();
        Task<sala_cine> GetById(int id);
        Task<sala_cine> Create(sala_cine sala);
        Task<sala_cine> Update(sala_cine sala);
        Task<bool> Delete(int id);
        Task<sala_cine> GetByNombre(string nombre);
        Task<int> CountPeliculasBySala(int salaId);
        Task<IEnumerable<pelicula>> GetPeliculasBySalaId(int salaId);
    }
}
