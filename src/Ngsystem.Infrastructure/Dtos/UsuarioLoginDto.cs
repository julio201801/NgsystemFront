using System.ComponentModel.DataAnnotations;

namespace Ngsystem.Infrastructure.Dtos;

public class UsuarioLoginDto
{
    [Required(ErrorMessage = "El correo es requerido.")]
    public string Usuario { get; set; } = "julio@com.pe";

    [Required(ErrorMessage = "La contraseña es requerida.")]
    public string Password { get; set; } = "Calculo2#2025";
}

public record LoginResponseDto(string Usuario, string Token);