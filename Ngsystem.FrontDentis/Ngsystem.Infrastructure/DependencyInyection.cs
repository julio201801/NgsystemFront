using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ngsystem.Infrastructure.Infrastructure.Http;
using Refit;

namespace Ngsystem.Infrastructure;

public static class DependencyInyection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //// Get the current configuration file.
        var baseUrl = configuration["BaseUrls:ApiBase"];
        Console.WriteLine(baseUrl);
        //var url = "http://localhost:5245";
        //var baseUrls = configuration.GetSection(BaseUrlConfiguration.CONFIG_NAME).<BaseUrlConfiguration>();
        services.AddRefitClient<IPaciente>().ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl));
        


        return services;
    }
}
