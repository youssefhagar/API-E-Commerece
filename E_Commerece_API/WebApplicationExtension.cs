using E_Commerece.Domain.Contract;

namespace E_Commerece.API
{
    public static class WebApplicationExtension
    {

        public static async Task<WebApplication> SeedDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");

            await seeder.SeedDataAsync();

            return app;
        }

    }
}
