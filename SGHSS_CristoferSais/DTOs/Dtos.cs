namespace SGHSS_CristoferSais.DTOs
{
    public record LoginDto(string Email, string Senha);
    public record RegisterPacienteDto(string Nome, string Cpf, string Email, string Senha, DateTime DataNascimento);
    public record RegisterConsultaDto(int PacienteId, int ProfissionalId, DateTime DataHora, bool IsTeleconsulta);
    public record RegisterProfissionalDto(string Nome, string Crm, string Especialidade, string Email, string Senha);
}
