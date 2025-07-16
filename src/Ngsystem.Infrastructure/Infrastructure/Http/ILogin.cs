using Ngsystem.Infrastructure.Dtos;
using Refit;

namespace Ngsystem.Infrastructure.Infrastructure.Http;

public interface ILogin
{

    [Post("/Usuario/Authentication")]
    Task<ResultadoDTO<LoginResponseDto>> Authentication(UsuarioLoginDto oreg);
}
