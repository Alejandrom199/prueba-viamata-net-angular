using cineManagementDatabaseFirst.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cineManagementDatabaseFirst.Services
{
    public interface ISalaCineService
    {
        Task<IEnumerable<SalaCineDTO>> GetAll();
        Task<SalaCineDTO> GetById(int id);
        Task<SalaCineDTO> Create(SalaCineCreateDTO salaDto);
        Task<SalaCineDTO> Update(SalaCineUpdateDTO salaDto);
        Task<bool> Deactivate(int id);
        Task<bool> Delete(int id);
        Task<SalaEstadoDTO> GetEstadoByNombre(string nombre);
        Task<DashboardDTO> GetDashboardData();
    }
}
