using cineManagementDatabaseFirst.Contexts;
using cineManagementDatabaseFirst.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cineManagementDatabaseFirst.Repository.impl
{
    public class PeliculaSalaCineRepository : IPeliculaSalaCineRepository
    {
        private readonly CineManagementDbContext _context;

        public PeliculaSalaCineRepository(CineManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<pelicula_sala_cine>> GetAll()
        {
            return await _context.pelicula_sala_cines
                .Include(ps => ps.Pelicula)
                .Include(ps => ps.Sala)
                .Where(ps => ps.Pelicula.EsActivo == true && ps.Sala.EsActivo == true)
                .ToListAsync();
        }

        public async Task<pelicula_sala_cine> GetById(int id)
        {
            return await _context.pelicula_sala_cines
                .Include(ps => ps.Pelicula)
                .Include(ps => ps.Sala)
                .FirstOrDefaultAsync(ps => ps.PeliculaSalaCineId == id);
        }

        public async Task<pelicula_sala_cine> Create(pelicula_sala_cine asignacion)
        {
            _context.pelicula_sala_cines.Add(asignacion);
            await _context.SaveChangesAsync();
            return asignacion;
        }

        public async Task<pelicula_sala_cine> Update(pelicula_sala_cine asignacion)
        {
            _context.Entry(asignacion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return asignacion;
        }

        public async Task<bool> Delete(int id)
        {
            var asignacion = await GetById(id);
            if (asignacion == null)
                return false;

            _context.pelicula_sala_cines.Remove(asignacion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<pelicula_sala_cine>> GetBySalaId(int salaId)
        {
            return await _context.pelicula_sala_cines
                .Include(ps => ps.Pelicula)
                .Include(ps => ps.Sala)
                .Where(ps => ps.SalaId == salaId && ps.Pelicula.EsActivo == true && ps.Sala.EsActivo == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<pelicula_sala_cine>> GetByPeliculaId(int peliculaId)
        {
            return await _context.pelicula_sala_cines
                .Include(ps => ps.Pelicula)
                .Include(ps => ps.Sala)
                .Where(ps => ps.PeliculaId == peliculaId && ps.Pelicula.EsActivo == true && ps.Sala.EsActivo == true)
                .ToListAsync();
        }

        public async Task<bool> ExistsConflict(int salaId, DateTime fechaInicio, DateTime fechaFin, int? excludeId = null)
        {
            var query = _context.pelicula_sala_cines
                .Where(ps => ps.SalaId == salaId &&
                    ((fechaInicio >= ps.FechaPublicacion && fechaInicio < ps.FechaFin) ||
                     (fechaFin > ps.FechaPublicacion && fechaFin <= ps.FechaFin) ||
                     (fechaInicio <= ps.FechaPublicacion && fechaFin >= ps.FechaFin)));

            if (excludeId.HasValue)
            {
                query = query.Where(ps => ps.PeliculaSalaCineId != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
}
