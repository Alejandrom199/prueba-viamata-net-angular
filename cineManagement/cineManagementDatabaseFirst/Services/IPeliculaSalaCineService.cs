using cineManagementDatabaseFirst.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cineManagementDatabaseFirst.Services
{
    public interface IPeliculaSalaCineService
    {
        Task<IEnumerable<PeliculaSalaCineDTO>> GetAll();
        Task<PeliculaSalaCineDTO> GetById(int id);
        Task<PeliculaSalaCineDTO> Create(PeliculaSalaCineCreateDTO asignacionDto);
        Task<PeliculaSalaCineDTO> Update(int id, PeliculaSalaCineUpdateDTO asignacionDto);
        Task<bool> Delete(int id);
        Task<IEnumerable<PeliculaSalaCineDTO>> GetBySalaId(int salaId);
        Task<IEnumerable<PeliculaSalaCineDTO>> GetByPeliculaId(int peliculaId);
        Task<bool> CheckDisponibilidad(int salaId, DateTime inicio, DateTime fin);
    }
}
