using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGHSS_CristoferSais.Data;
using SGHSS_CristoferSais.DTOs;
using SGHSS_CristoferSais.Models;

namespace SGHSS_CristoferSais.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConsultasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize] // Exige login
        public async Task<IActionResult> Agendar([FromBody] RegisterConsultaDto dto)
        {
            var consulta = new Consulta
            {
                PacienteId = dto.PacienteId,
                ProfissionalId = dto.ProfissionalId,
                DataHora = dto.DataHora,
                Status = "Agendada",
                IsTeleconsulta = dto.IsTeleconsulta
            };

            //OBs: Se for Teleconsulta, gera link
            if (dto.IsTeleconsulta)
            {
                consulta.LinkSalaVirtual = $"https://vidaplus.telemedicina.com/sala/{Guid.NewGuid()}";
            }

            _context.Consultas.Add(consulta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = consulta.Id }, consulta);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _context.Consultas.FindAsync(id));
        }
    }
}
