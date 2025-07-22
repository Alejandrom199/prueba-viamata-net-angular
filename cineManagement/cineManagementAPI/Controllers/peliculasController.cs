using cineManagementDatabaseFirst.Contexts;
using cineManagementDatabaseFirst.Models.DTOs;
using cineManagementDatabaseFirst.Models.Entities;
using cineManagementDatabaseFirst.Repository;
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
    public class peliculasController : ControllerBase
    {
        private readonly IPeliculaService _service;

        public peliculasController(IPeliculaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeliculaDTO>>> GetAll()
        {
            var peliculas = await _service.GetAll();
            return Ok(peliculas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PeliculaDTO>> GetById(int id)
        {
            var pelicula = await _service.GetById(id);
            if (pelicula == null)
                return NotFound();

            return Ok(pelicula);
        }

        [HttpPost]
        public async Task<ActionResult<PeliculaDTO>> Create([FromBody] PeliculaCreateDTO peliculaDto)
        {
            var nuevaPelicula = await _service.Create(peliculaDto);
            return CreatedAtAction(nameof(GetById), new { id = nuevaPelicula.PeliculaId }, nuevaPelicula);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PeliculaUpdateDTO peliculaDto)
        {

            if (id != peliculaDto.PeliculaId)
                return BadRequest();

            var pelicula = await _service.Update(peliculaDto);
            if (pelicula == null)
                return NotFound();

            return NoContent();
        }

        [HttpPut("desactivar/{id}")]
        public async Task<IActionResult> Deactive(int id)
        {
            var success = await _service.Deactivate(id);
            return success ? NoContent() : NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        //LinQ
        /*[HttpGet("buscar/{nombre}")]
        public async Task<ActionResult<IEnumerable<PeliculaDTO>>> BuscarPorNombre(string nombre)
        {
            var peliculas = await _service.BuscarPorNombre(nombre);
            return Ok(peliculas);
        }

        [HttpGet("por-fecha/{fecha}")]
        public async Task<ActionResult<IEnumerable<PeliculaDTO>>> GetByFechaPublicacion(DateTime fecha)
        {
            var peliculas = await _service.GetByFechaPublicacion(fecha);
            return Ok(peliculas);
        }*/

        //Stores Procedures
        [HttpGet("buscar/{nombre}")]
        public async Task<ActionResult<IEnumerable<PeliculaConSalasDTO>>> BuscarPorNombreSP(string nombre)
        {
            try
            {
                var result = await _service.BuscarPeliculaPorNombreSP(nombre);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("por-fecha/{fecha}")]
        public async Task<ActionResult<IEnumerable<PeliculaConDetallesDTO>>> GetPorFechaSP(DateTime fecha)
        {
            try
            {
                var result = await _service.ObtenerPeliculasPorFechaSP(fecha);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
