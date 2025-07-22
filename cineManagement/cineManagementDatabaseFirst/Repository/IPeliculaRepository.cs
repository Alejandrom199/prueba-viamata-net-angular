using cineManagementDatabaseFirst.Models.DTOs;
using cineManagementDatabaseFirst.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cineManagementDatabaseFirst.Repository
{
    public interface IPeliculaRepository
    {
        Task<IEnumerable<pelicula>> GetAll();
        Task<pelicula> GetById(int id);
        Task<pelicula> Create(pelicula pelicula);
        Task<pelicula> Update(pelicula pelicula);
        Task<bool> Delete(int id);
        //Task<IEnumerable<pelicula>> BuscarPorNombre(string nombre);
        //Task<IEnumerable<pelicula>> GetByFechaPublicacion(DateTime fecha);

        Task<bool> peliculaExists(int id);
        Task<IEnumerable<PeliculaConSalasDTO>> BuscarPeliculaPorNombreSP(string nombre);
        Task<IEnumerable<PeliculaConDetallesDTO>> ObtenerPeliculasPorFechaSP(DateTime fecha);
        Task<int> GetTotalCount();
    }
}
