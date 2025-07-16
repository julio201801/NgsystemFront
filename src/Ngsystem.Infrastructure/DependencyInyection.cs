using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ngsystem.Infrastructure.Infrastructure.Http;
using Refit;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
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
        //services.AddRefitClient<IPaciente>().ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl));
        services.AddRefitClient<ILogin>().ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl));

        services.AddRefitClient<IPaciente>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl))
                .AddHttpMessageHandler<AuthHeaderHandler>();

        return services;
    }
}
public class AuthHeaderHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;
    public AuthHeaderHandler(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        return await base.SendAsync(request, cancellationToken);
    }
}