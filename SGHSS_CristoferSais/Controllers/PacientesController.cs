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
    public class PacientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PacientesController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint Público para criar conta (Sign-up)
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegisterPacienteDto dto)
        {
            // 1. Criar Usuário de Login
            var usuario = new Usuario
            {
                Email = dto.Email,
                Senha = dto.Senha, // Simulação. Use BCrypt na vida real.
                Funcao = "Paciente"
            };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // 2. Criar Perfil de Paciente
            var paciente = new Paciente
            {
                Nome = dto.Nome,
                Cpf = dto.Cpf,
                DataNascimento = dto.DataNascimento,
                UsuarioId = usuario.Id
            };
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = paciente.Id }, paciente);
        }

        // Endpoint Protegido (Só com Cadeado/Token)
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Pacientes.ToListAsync());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null) return NotFound();
            return Ok(paciente);
        }
    }
}
