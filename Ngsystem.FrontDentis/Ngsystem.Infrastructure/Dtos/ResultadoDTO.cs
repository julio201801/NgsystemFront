
namespace Ngsystem.Infrastructure.Dtos;
public class ResultadoDTO<T>
{
    public bool Status { get; set; }
    public string? Mensaje { get; set; }
    public IEnumerable<T>? Lista { get; set; }
    public T? Registro { get; set; }
}
