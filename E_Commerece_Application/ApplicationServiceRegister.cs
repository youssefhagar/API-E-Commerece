using E_Commerece.Application.Contracts;
using E_Commerece.Application.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Application
{
    public static class ApplicationServiceRegister
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(X=> { },typeof(ApplicationServiceRegister).Assembly);
            services.AddScoped<IProductService, ProductService>();
            return services;
        }

    }
}
