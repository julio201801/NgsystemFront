using Ngsystem.Infrastructure.Dtos;
using Refit;

namespace Ngsystem.Infrastructure.Infrastructure.Http;

public interface IPaciente
{
    [Get("/Paciente/ListaPaciente")]
    Task<ResultadoDTO<LisPacienteResponseDto>> ListaPaciente();

    [Post("/Paciente/GrabarPaciente")]
    Task<ResultadoDTO<LisPacienteResponseDto>> GrabarPaciente(LisPacienteResponseDto oreg);
    [Put("/Paciente/UpdatePaciente")]
    Task<ResultadoDTO<LisPacienteResponseDto>> UpdatePaciente(LisPacienteResponseDto oreg);
}
