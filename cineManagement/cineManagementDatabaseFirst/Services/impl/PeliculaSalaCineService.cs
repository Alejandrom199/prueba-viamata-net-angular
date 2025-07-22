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
    public class PeliculaSalaCineService : IPeliculaSalaCineService
    {
        private readonly IPeliculaSalaCineRepository _repository;
        private readonly IPeliculaRepository _peliculaRepository;
        private readonly ISalaCineRepository _salaRepository;

        public PeliculaSalaCineService(
            IPeliculaSalaCineRepository repository,
            IPeliculaRepository peliculaRepository,
            ISalaCineRepository salaRepository)
        {
            _repository = repository;
            _peliculaRepository = peliculaRepository;
            _salaRepository = salaRepository;
        }

        public async Task<IEnumerable<PeliculaSalaCineDTO>> GetAll()
        {
            var asignaciones = await _repository.GetAll();
            return asignaciones.Select(MapToDTO);
        }

        public async Task<PeliculaSalaCineDTO> GetById(int id)
        {
            var asignacion = await _repository.GetById(id);
            return asignacion == null ? null : MapToDTO(asignacion);
        }

        public async Task<PeliculaSalaCineDTO> Create(PeliculaSalaCineCreateDTO asignacionDto)
        {
            ValidateDates(asignacionDto.FechaPublicacion, asignacionDto.FechaFin);

            var pelicula = await ValidatePelicula(asignacionDto.PeliculaId);
            var sala = await ValidateSala(asignacionDto.SalaId);

            await ValidateScheduleConflict(asignacionDto.SalaId,
                asignacionDto.FechaPublicacion,
                asignacionDto.FechaFin);

            var asignacion = new pelicula_sala_cine
            {
                PeliculaId = asignacionDto.PeliculaId,
                SalaId = asignacionDto.SalaId,
                FechaPublicacion = asignacionDto.FechaPublicacion,
                FechaFin = asignacionDto.FechaFin
            };

            var created = await _repository.Create(asignacion);
            return MapToDTO(created);
        }

        public async Task<PeliculaSalaCineDTO> Update(int id, PeliculaSalaCineUpdateDTO asignacionDto)
        {
            if (id != asignacionDto.PeliculaSalaCineId)
                throw new ArgumentException("ID en URL no coincide con ID en body");

            var existing = await _repository.GetById(id);
            if (existing == null)
                return null;

            ValidateDates(asignacionDto.FechaPublicacion, asignacionDto.FechaFin);

            await ValidatePelicula(asignacionDto.PeliculaId);
            await ValidateSala(asignacionDto.SalaId);

            await ValidateScheduleConflict(asignacionDto.SalaId,
                asignacionDto.FechaPublicacion,
                asignacionDto.FechaFin,
                id);

            existing.PeliculaId = asignacionDto.PeliculaId;
            existing.SalaId = asignacionDto.SalaId;
            existing.FechaPublicacion = asignacionDto.FechaPublicacion;
            existing.FechaFin = asignacionDto.FechaFin;

            var updated = await _repository.Update(existing);
            return MapToDTO(updated);
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<IEnumerable<PeliculaSalaCineDTO>> GetBySalaId(int salaId)
        {
            var asignaciones = await _repository.GetBySalaId(salaId);
            return asignaciones.Select(MapToDTO);
        }

        public async Task<IEnumerable<PeliculaSalaCineDTO>> GetByPeliculaId(int peliculaId)
        {
            var asignaciones = await _repository.GetByPeliculaId(peliculaId);
            return asignaciones.Select(MapToDTO);
        }

        public async Task<bool> CheckDisponibilidad(int salaId, DateTime inicio, DateTime fin)
        {
            return !await _repository.ExistsConflict(salaId, inicio, fin);
        }

        // Métodos privados de ayuda
        private void ValidateDates(DateTime inicio, DateTime fin)
        {
            if (fin <= inicio)
                throw new ArgumentException("La fecha de fin debe ser posterior a la fecha de inicio");
        }

        private async Task<pelicula> ValidatePelicula(int peliculaId)
        {
            var pelicula = await _peliculaRepository.GetById(peliculaId);
            if (pelicula == null || !pelicula.EsActivo.GetValueOrDefault())
                throw new KeyNotFoundException("Película no encontrada o no está activa");
            return pelicula;
        }

        private async Task<sala_cine> ValidateSala(int salaId)
        {
            var sala = await _salaRepository.GetById(salaId);
            if (sala == null || !sala.EsActivo.GetValueOrDefault())
                throw new KeyNotFoundException("Sala no encontrada o no está activa");
            return sala;
        }

        private async Task ValidateScheduleConflict(int salaId, DateTime inicio, DateTime fin, int? excludeId = null)
        {
            if (await _repository.ExistsConflict(salaId, inicio, fin, excludeId))
                throw new InvalidOperationException("Conflicto de horario: la sala ya tiene una película programada en ese horario");
        }

        private PeliculaSalaCineDTO MapToDTO(pelicula_sala_cine asignacion)
        {
            return new PeliculaSalaCineDTO
            {
                PeliculaSalaCineId = asignacion.PeliculaSalaCineId,
                PeliculaId = asignacion.PeliculaId,
                SalaId = asignacion.SalaId,
                FechaPublicacion = asignacion.FechaPublicacion,
                FechaFin = asignacion.FechaFin,
                Pelicula = asignacion.Pelicula == null ? null : new PeliculaDTO
                {
                    PeliculaId = asignacion.Pelicula.PeliculaId,
                    Nombre = asignacion.Pelicula.Nombre,
                    Descripcion = asignacion.Pelicula.Descripcion,
                    Duracion = asignacion.Pelicula.Duracion,
                    Imagen = asignacion.Pelicula.Imagen,
                    EsActivo = asignacion.Pelicula.EsActivo
                },
                Sala = asignacion.Sala == null ? null : new SalaCineDTO
                {
                    SalaId = asignacion.Sala.SalaId,
                    Nombre = asignacion.Sala.Nombre,
                    Estado = asignacion.Sala.Estado,
                    EsActivo = asignacion.Sala.EsActivo
                }
            };
        }
    }
}
