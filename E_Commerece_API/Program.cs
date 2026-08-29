using E_Commerece.API;
using E_Commerece.API.Features.Orders.Payment;
using E_Commerece.Application;
using E_Commerece.Infrastructure;
using ECommerce.API.Endpoints;
using Microsoft.Extensions.FileProviders;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddApplicationServices();
        builder.Services.Configure<UrlSettings>(builder.Configuration.GetSection("UrlSettings"));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        await app.SeedDataAsync();
        app.UseHttpsRedirection();

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
            app.UseSwagger();
            app.UseSwaggerUI();
        //}
        app.UseHttpsRedirection();
        var filesPath = Path.Combine(builder.Environment.ContentRootPath, "Files");

        Directory.CreateDirectory(filesPath);

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(filesPath),
            RequestPath = "/Files"
        });

        

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        // Minimal API Endpoints
        app.MapPaymentEndpoints();
        app.MapCreateOrderPaymentEndpoint();

        app.Run();
    }
}