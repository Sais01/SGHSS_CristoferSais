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
    public class ProfissionaisController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProfissionaisController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegisterProfissionalDto dto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Email já cadastrado.");

            var usuario = new Usuario
            {
                Email = dto.Email,
                Senha = dto.Senha, 
                Funcao = "Profissional"
            };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var profissional = new Profissional
            {
                Nome = dto.Nome,
                CRM = dto.Crm,
                Especialidade = dto.Especialidade,
                UsuarioId = usuario.Id
            };
            _context.Profissionais.Add(profissional);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = profissional.Id }, profissional);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Profissionais.ToListAsync());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var prof = await _context.Profissionais.FindAsync(id);
            if (prof == null) return NotFound();
            return Ok(prof);
        }
    }
}
