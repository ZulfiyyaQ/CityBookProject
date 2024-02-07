using CityBookMVCOnionApplication.Abstractions.Services;
using CityBookMVCOnionInfrastructure.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace CityBookMVCOnionInfrastructure.ServiceRegistrations
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
