using E_Commerece.Application.Contracts;
using E_Commerece.Application.Service;
using E_Commerece.Application.Service.Auth;
using E_Commerece.Domain.Contract;
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
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddSingleton<ICachService,CachService>();

            return services;
        }

    }
}
