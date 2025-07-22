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
    public class sala_cineController : ControllerBase
    {
        private readonly ISalaCineService _salaService;

        public sala_cineController(ISalaCineService salaService)
        {
            _salaService = salaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalaCineDTO>>> GetAll()
        {
            var salas = await _salaService.GetAll();
            return Ok(salas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalaCineDTO>> GetById(int id)
        {
            var sala = await _salaService.GetById(id);
            if (sala == null)
                return NotFound();
            return Ok(sala);
        }

        [HttpPost]
        public async Task<ActionResult<SalaCineDTO>> Create([FromBody] SalaCineCreateDTO salaDto)
        {
            var nuevaSala = await _salaService.Create(salaDto);
            return CreatedAtAction(nameof(GetById), new { id = nuevaSala.SalaId }, nuevaSala);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SalaCineUpdateDTO salaDto)
        {
            if (id != salaDto.SalaId)
                return BadRequest();

            var sala = await _salaService.Update(salaDto);
            if (sala == null)
                return NotFound();

            return NoContent();
        }

        [HttpPut("desactivar/{id}")]
        public async Task<IActionResult> Deactive(int id)
        {
            var success = await _salaService.Deactivate(id);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _salaService.Delete(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("estado/{nombre}")]
        public async Task<ActionResult<SalaEstadoDTO>> GetEstadoByNombre(string nombre)
        {
            var estado = await _salaService.GetEstadoByNombre(nombre);
            if (estado == null)
                return NotFound();
            return Ok(estado);
        }

        [HttpGet("dashboard")]
        public async Task<ActionResult<DashboardDTO>> GetDashboardData()
        {
            var datos = await _salaService.GetDashboardData();
            return Ok(datos);
        }
    }
}
