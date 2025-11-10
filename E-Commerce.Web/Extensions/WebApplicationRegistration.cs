using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace E_Commerce.Web.Extensions
{
    public static class WebApplicationRegistration
    {

        public static async Task<WebApplication> MigrateDataAsync(this WebApplication web)
        {
           await using var scope = web.Services.CreateAsyncScope();

            var dbcontextserv = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            var getPendingMig =await dbcontextserv.Database.GetPendingMigrationsAsync();
            if (getPendingMig.Any())
                dbcontextserv.Database.Migrate();

            return web;
        }

        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
          await  using var scope = app.Services.CreateAsyncScope();
            var DataInitial = scope.ServiceProvider.GetRequiredService<IDataInitializer>();
           await DataInitial.InitializeAsync();

            return app;
        }
    }
}
