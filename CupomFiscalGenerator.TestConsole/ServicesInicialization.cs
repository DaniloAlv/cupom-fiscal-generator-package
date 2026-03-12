using CupomFiscalGenerator;
using Microsoft.Extensions.DependencyInjection;

namespace CupomFiscalGenerator.TestConsole;

public static class ServicesInicialization
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddCupomFiscalGenerator();

        return services;
    }
}