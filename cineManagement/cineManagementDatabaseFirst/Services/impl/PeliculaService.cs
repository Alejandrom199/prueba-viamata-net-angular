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
    public class PeliculaService : IPeliculaService
    {
        private readonly IPeliculaRepository _repository;

        public PeliculaService(IPeliculaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PeliculaDTO>> GetAll()
        {
            var peliculas = await _repository.GetAll();
            return peliculas.Select(p => MapToDTO(p));
        }

        public async Task<PeliculaDTO> GetById(int id)
        {
            var pelicula = await _repository.GetById(id);
            return pelicula == null ? null : MapToDTO(pelicula);
        }

        public async Task<PeliculaDTO> Create(PeliculaCreateDTO peliculaDto)
        {
            var pelicula = new pelicula
            {
                Nombre = peliculaDto.Nombre,
                Descripcion = peliculaDto.Descripcion,
                Duracion = peliculaDto.Duracion,
                Imagen = peliculaDto.Imagen,
                EsActivo = true
            };

            var created = await _repository.Create(pelicula);
            return MapToDTO(created);
        }

        public async Task<PeliculaDTO> Update(PeliculaUpdateDTO peliculaDto)
        {
            var existing = await _repository.GetById(peliculaDto.PeliculaId);
            if (existing == null)
                return null;

            existing.Nombre = peliculaDto.Nombre;
            existing.Descripcion = peliculaDto.Descripcion;
            existing.Duracion = peliculaDto.Duracion;
            existing.Imagen = peliculaDto.Imagen;

            var updated = await _repository.Update(existing);
            return MapToDTO(updated);
        }

        public async Task<bool> Deactivate(int id)
        {
            var pelicula = await _repository.GetById(id);
            if (pelicula == null)
                return false;

            pelicula.EsActivo = false;
            await _repository.Update(pelicula);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.Delete(id);
        }

        //LinQ
        /*
        public async Task<IEnumerable<PeliculaDTO>> BuscarPorNombre(string nombre)
        {
            var peliculas = await _repository.BuscarPorNombre(nombre);
            return peliculas.Select(p => MapToDTO(p));
        }

        public async Task<IEnumerable<PeliculaDTO>> GetByFechaPublicacion(DateTime fecha)
        {
            var peliculas = await _repository.GetByFechaPublicacion(fecha);
            return peliculas.Select(p => MapToDTO(p));
        }
        */

        public async Task<bool> peliculaExists(int id)
        {
            return await _repository.peliculaExists(id);
        }
        
        //Stores Procedures
        public async Task<IEnumerable<PeliculaConSalasDTO>> BuscarPeliculaPorNombreSP(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de búsqueda no puede estar vacío");

            return await _repository.BuscarPeliculaPorNombreSP(nombre);
        }

        public async Task<IEnumerable<PeliculaConDetallesDTO>> ObtenerPeliculasPorFechaSP(DateTime fecha)
        {
            try
            {
                return await _repository.ObtenerPeliculasPorFechaSP(fecha);
            }
            catch (ArgumentException ex)
            {
                // Captura el error de fecha futura del SP
                throw;
            }
        }


        private PeliculaDTO MapToDTO(pelicula pelicula)
        {
            return new PeliculaDTO
            {
                PeliculaId = pelicula.PeliculaId,
                Nombre = pelicula.Nombre,
                Descripcion = pelicula.Descripcion,
                Duracion = pelicula.Duracion,
                Imagen = pelicula.Imagen,
                EsActivo = pelicula.EsActivo
            };
        }

        private pelicula MapToEntity(PeliculaCreateDTO dto)
        {
            return new pelicula
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Duracion = dto.Duracion,
                Imagen = dto.Imagen,
                EsActivo = true
            };
        }
    }
}
