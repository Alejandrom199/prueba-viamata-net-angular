using cineManagementDatabaseFirst.Models.DTOs;
using cineManagementDatabaseFirst.Models.Entities;
using cineManagementDatabaseFirst.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cineManagementDatabaseFirst.Services.impl
{
    public class SalaCineService : ISalaCineService
    {
        private readonly ISalaCineRepository _repository;
        private readonly IPeliculaRepository _peliculaRepository;

        public SalaCineService(ISalaCineRepository repository, IPeliculaRepository peliculaRepository)
        {
            _repository = repository;
            _peliculaRepository = peliculaRepository;
        }

        public async Task<IEnumerable<SalaCineDTO>> GetAll()
        {

            var salas = await _repository.GetAll();
            return salas.Select(s => MapToDTO(s));
        }

        public async Task<SalaCineDTO> GetById(int id)
        {
            var sala = await _repository.GetById(id);
            return sala == null ? null : MapToDTO(sala);
        }

        public async Task<SalaCineDTO> Create(SalaCineCreateDTO salaDto)
        {
            var sala = new sala_cine
            {
                Nombre = salaDto.Nombre,
                Estado = salaDto.Estado,
                EsActivo = true
            };

            var created = await _repository.Create(sala);
            return MapToDTO(created);
        }

        public async Task<SalaCineDTO> Update(SalaCineUpdateDTO salaDto)
        {
            var existing = await _repository.GetById(salaDto.SalaId);
            if (existing == null)
                return null;

            existing.Nombre = salaDto.Nombre;
            existing.Estado = salaDto.Estado;

            var updated = await _repository.Update(existing);
            return MapToDTO(updated);
        }

        public async Task<bool> Deactivate(int id)
        {
            var sala_cine = await _repository.GetById(id);
            if (sala_cine == null)
                return false;

            sala_cine.EsActivo = false;
            await _repository.Update(sala_cine);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<SalaEstadoDTO> GetEstadoByNombre(string nombre)
        {
            var sala = await _repository.GetByNombre(nombre);
            if (sala == null)
                return null;

            var cantidadPeliculas = await _repository.CountPeliculasBySala(sala.SalaId);
            var peliculas = await _repository.GetPeliculasBySalaId(sala.SalaId);

            return new SalaEstadoDTO
            {
                SalaId = sala.SalaId,
                Nombre = sala.Nombre,
                Estado = sala.Estado,
                CantidadPeliculas = cantidadPeliculas,
                EstadoSala = cantidadPeliculas switch
                {
                    < 3 => "Sala disponible",
                    >= 3 and <= 5 => $"Sala con {cantidadPeliculas} películas asignadas",
                    > 5 => "Sala no disponible"
                },
                Peliculas = peliculas.Select(p => new PeliculaDTO
                {
                    PeliculaId = p.PeliculaId,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    Duracion = p.Duracion,
                    Imagen = p.Imagen,
                    EsActivo = p.EsActivo
                }).ToList()
            };
        }

        public async Task<DashboardDTO> GetDashboardData()
        {
            var salas = await _repository.GetAll();
            var totalSalas = salas.Count();

            var salasDisponibles = new List<sala_cine>();
            foreach (var sala in salas)
            {
                var count = await _repository.CountPeliculasBySala(sala.SalaId);
                if (count < 3)
                    salasDisponibles.Add(sala);
            }

            var totalPeliculas = await _peliculaRepository.GetTotalCount();

            return new DashboardDTO
            {
                TotalSalas = totalSalas,
                TotalSalasDisponibles = salasDisponibles.Count,
                TotalPeliculas = totalPeliculas,
            };
        }

        private SalaCineDTO MapToDTO(sala_cine sala)
        {
            return new SalaCineDTO
            {
                SalaId = sala.SalaId,
                Nombre = sala.Nombre,
                Estado = sala.Estado,
                EsActivo = sala.EsActivo
            };
        }
    }
}
