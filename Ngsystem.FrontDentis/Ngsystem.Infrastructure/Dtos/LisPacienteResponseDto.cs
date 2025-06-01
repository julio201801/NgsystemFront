using System.ComponentModel.DataAnnotations;

namespace Ngsystem.Infrastructure.Dtos;

public class LisPacienteResponseDto
{
    public int? Id { get; set; }
    public string? Nombre { get; set; }  
    public string? Apellido { get; set; }
    public string? FechaNacimiento { get; set; }
    public string? Genero { get; set; }
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }
    public string? Dni { get; set; }

}
public class ItemPacienteResponseDto
{
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? FechaNacimiento { get; set; }
    public string? Genero { get; set; }
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }
    public string? Dni { get; set; }

}