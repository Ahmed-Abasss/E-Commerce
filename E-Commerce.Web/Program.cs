
using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data.DataSeeding;
using E_Commerce.Persistence.Data.DbContexts;
using E_Commerce.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options=>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IDataInitializer, DataInitializer>();
            var app = builder.Build();
            #region Data Seeding

            await app.MigrateDataAsync();
             await   app.SeedDatabaseAsync();

            #endregion
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

           await app.RunAsync();
        }
    }
}
