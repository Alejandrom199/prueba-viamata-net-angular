using cineManagementDatabaseFirst.Contexts;
using cineManagementDatabaseFirst.Models.DTOs;
using cineManagementDatabaseFirst.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cineManagementDatabaseFirst.Repository.impl
{
    public class PeliculaRepository : IPeliculaRepository
    {
        private readonly CineManagementDbContext _context;

        public PeliculaRepository(CineManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<pelicula>> GetAll()
        {
            return await _context.peliculas
                .Where(p => p.EsActivo == true)
                .ToListAsync();
        }

        public async Task<pelicula> GetById(int id)
        {
            return await _context.peliculas
                .FirstOrDefaultAsync(p => p.PeliculaId == id && p.EsActivo == true);
        }

        public async Task<pelicula> Create(pelicula pelicula)
        {
            pelicula.EsActivo = true;
            _context.peliculas.Add(pelicula);
            await _context.SaveChangesAsync();
            return pelicula;
        }

        public async Task<pelicula> Update(pelicula pelicula)
        {
            _context.peliculas.Update(pelicula);
            await _context.SaveChangesAsync();
            return pelicula;
        }

        public async Task<bool> Delete(int id)
        {
            var pelicula = await GetById(id);
            if (pelicula == null)
                return false;

            pelicula.EsActivo = false;
            await _context.SaveChangesAsync();
            return true;
        }

        //LinQ
        /*
        public async Task<IEnumerable<pelicula>> BuscarPorNombre(string nombre)
        {
            return await _context.peliculas
                .Where(p => p.Nombre.Contains(nombre) && p.EsActivo == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<pelicula>> GetByFechaPublicacion(DateTime fecha)
        {
            return await _context.pelicula_sala_cines
                .Where(ps => ps.FechaPublicacion.Date == fecha.Date)
                .Include(ps => ps.Pelicula)
                .Where(ps => ps.Pelicula.EsActivo == true)
                .Select(ps => ps.Pelicula)
                .Distinct()
                .ToListAsync();
        }
        */

        public Task<bool> peliculaExists(int id)
        {
            return _context.peliculas.AnyAsync(p => p.PeliculaId == id);
        }

        //Stored Procedures
        public async Task<IEnumerable<PeliculaConSalasDTO>> BuscarPeliculaPorNombreSP(string nombre)
        {
            var nombreParam = new SqlParameter("@Nombre", nombre);

            return await _context.Set<PeliculaConSalasDTO>()
                .FromSqlRaw("EXEC sp_BuscarPeliculaPorNombre @Nombre", nombreParam)
                .ToListAsync();
        }

        public async Task<IEnumerable<PeliculaConDetallesDTO>> ObtenerPeliculasPorFechaSP(DateTime fecha)
        {
            try
            {
                var fechaParam = new SqlParameter("@FechaPublicacion", fecha.Date);

                return await _context.Set<PeliculaConDetallesDTO>()
                    .FromSqlRaw("EXEC sp_ObtenerPeliculasPorFechaPublicacion @FechaPublicacion", fechaParam)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Capturar el error específico del SP
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 50000)
                {
                    throw new ArgumentException(sqlEx.Message);
                }
                throw;
            }
        }
        public async Task<int> GetTotalCount()
        {
            return await _context.peliculas
                .Where(p => p.EsActivo == true)
                .CountAsync();
        }
    }
}
