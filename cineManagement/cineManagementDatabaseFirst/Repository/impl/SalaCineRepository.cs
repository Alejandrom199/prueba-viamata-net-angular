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
    public class SalaCineRepository : ISalaCineRepository
    {
        private readonly CineManagementDbContext _context;

        public SalaCineRepository(CineManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<sala_cine>> GetAll()
        {
            return await _context.sala_cines
                .Where(s => s.EsActivo == true)
                .ToListAsync();
        }

        public async Task<sala_cine> GetById(int id)
        {
            return await _context.sala_cines
                .FirstOrDefaultAsync(s => s.SalaId == id && s.EsActivo == true);
        }

        public async Task<sala_cine> Create(sala_cine sala)
        {
            sala.EsActivo = true;
            _context.sala_cines.Add(sala);
            await _context.SaveChangesAsync();
            return sala;
        }

        public async Task<sala_cine> Update(sala_cine sala)
        {
            _context.sala_cines.Update(sala);
            await _context.SaveChangesAsync();
            return sala;
        }

        public async Task<bool> Delete(int id)
        {
            var sala = await GetById(id);
            if (sala == null)
                return false;

            sala.EsActivo = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<sala_cine> GetByNombre(string nombre)
        {
            return await _context.sala_cines
                .FirstOrDefaultAsync(s => s.Nombre == nombre && s.EsActivo == true);
        }

        public async Task<int> CountPeliculasBySala(int salaId)
        {
            return await _context.pelicula_sala_cines
                .CountAsync(ps => ps.SalaId == salaId);
        }

        public async Task<IEnumerable<pelicula>> GetPeliculasBySalaId(int salaId)
        {
            return await _context.pelicula_sala_cines
                .Where(ps => ps.SalaId == salaId)
                .Include(ps => ps.Pelicula)
                .Select(ps => ps.Pelicula)
                .ToListAsync();
        }
    }
}
