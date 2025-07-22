using cineManagementDatabaseFirst.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cineManagementDatabaseFirst.Services
{
    public interface IPeliculaService
    {
        Task<IEnumerable<PeliculaDTO>> GetAll();
        Task<PeliculaDTO> GetById(int id);
        Task<PeliculaDTO> Create(PeliculaCreateDTO peliculaDto);
        Task<PeliculaDTO> Update(PeliculaUpdateDTO peliculaDto);
        Task<bool> Deactivate(int id);
        Task<bool> Delete(int id);
        //Task<IEnumerable<PeliculaDTO>> BuscarPorNombre(string nombre);
        //Task<IEnumerable<PeliculaDTO>> GetByFechaPublicacion(DateTime fecha);
        Task<bool> peliculaExists(int id);
        Task<IEnumerable<PeliculaConSalasDTO>> BuscarPeliculaPorNombreSP(string nombre);
        Task<IEnumerable<PeliculaConDetallesDTO>> ObtenerPeliculasPorFechaSP(DateTime fecha);
    }
}
