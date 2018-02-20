using System;
using Microsoft.Extensions.DependencyInjection;
using CarrosUsadosAzureSearch.Application;
using CarrosUsadosAzureSearch.Application.Services;
using Crawler.Core.Interface.Repository;
using CarrosUsadosAzureSearch.Infra.Data;

namespace CarrosUsadosAzureSearch.Infra.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICarroService, CarroService>();
            services.AddScoped<ICarroRepository, CarroRepository>();
        }
    }
}
