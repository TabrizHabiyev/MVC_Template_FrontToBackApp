using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVC_TemplateApp.Aplication.Abstraction.Services;
using MVC_TemplateApp.Infrastructure.Services;

namespace MVC_TemplateApp.Infrastructure
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
           services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}