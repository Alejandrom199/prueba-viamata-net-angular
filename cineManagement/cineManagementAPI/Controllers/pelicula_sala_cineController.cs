using cineManagementDatabaseFirst.Contexts;
using cineManagementDatabaseFirst.Models.DTOs;
using cineManagementDatabaseFirst.Models.Entities;
using cineManagementDatabaseFirst.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cineManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasSalasCineController : ControllerBase
    {
        private readonly IPeliculaSalaCineService _service;

        public PeliculasSalasCineController(IPeliculaSalaCineService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeliculaSalaCineDTO>>> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PeliculaSalaCineDTO>> GetById(int id)
        {
            var result = await _service.GetById(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<PeliculaSalaCineDTO>> Create(PeliculaSalaCineCreateDTO dto)
        {
            try
            {
                var result = await _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.PeliculaSalaCineId }, result);
            }
            catch (Exception ex) when (ex is ArgumentException ||
                                     ex is KeyNotFoundException ||
                                     ex is InvalidOperationException)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PeliculaSalaCineUpdateDTO dto)
        {
            try
            {
                var result = await _service.Update(id, dto);
                return result == null ? NotFound() : NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) when (ex is KeyNotFoundException || ex is InvalidOperationException)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            return result ? NoContent() : NotFound();
        }

        [HttpGet("por-sala/{salaId}")]
        public async Task<ActionResult<IEnumerable<PeliculaSalaCineDTO>>> GetBySalaId(int salaId)
        {
            return Ok(await _service.GetBySalaId(salaId));
        }

        [HttpGet("por-pelicula/{peliculaId}")]
        public async Task<ActionResult<IEnumerable<PeliculaSalaCineDTO>>> GetByPeliculaId(int peliculaId)
        {
            return Ok(await _service.GetByPeliculaId(peliculaId));
        }

        [HttpGet("disponibilidad")]
        public async Task<ActionResult<bool>> CheckDisponibilidad(
            [FromQuery] int salaId,
            [FromQuery] DateTime inicio,
            [FromQuery] DateTime fin)
        {
            return Ok(await _service.CheckDisponibilidad(salaId, inicio, fin));
        }
    }
}
