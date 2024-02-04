using CityBookMVCOnionApplication.Validators.Tag;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CityBookMVCOnionApplication.ServiceRegistrations
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining(typeof(CreateTagVMValidator)));
            return services;
        }
    }
}
