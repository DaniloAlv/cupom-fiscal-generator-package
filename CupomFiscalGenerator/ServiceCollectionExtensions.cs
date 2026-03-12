using CupomFiscalGenerator.Services;
using CupomFiscalGenerator.Services.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace CupomFiscalGenerator
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCupomFiscalGenerator(this IServiceCollection services)
        {
            services.AddScoped<ICupomFiscalGenerator, GeneratorService>();
            return services;
        }
    }
}
