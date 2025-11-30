using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SGHSS_CristoferSais.Models
{
    // Entidade central de Login (Segurança/LGPD)
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [JsonIgnore] // Para não retornar a senha na API
        public string Senha { get; set; } = string.Empty;

        public string Funcao { get; set; } = "Paciente"; // "Paciente", "Profissional", "Admin"
    }

    public class Paciente
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }

        // Relacionamento com Usuario (Login)
        public int UsuarioId { get; set; }
        [JsonIgnore]
        public Usuario? Usuario { get; set; }
    }

    public class Profissional
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CRM { get; set; } = string.Empty;
        public string Especialidade { get; set; } = string.Empty;

        // Relacionamento com Usuario (Login)
        public int UsuarioId { get; set; }
        [JsonIgnore]
        public Usuario? Usuario { get; set; }
    }

    public class Consulta
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Status { get; set; } = "Agendada";
        public bool IsTeleconsulta { get; set; }
        public string? LinkSalaVirtual { get; set; }

        public int PacienteId { get; set; }
        public int ProfissionalId { get; set; }
    }
}
