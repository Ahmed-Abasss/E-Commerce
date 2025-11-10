using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.DataSeeding
{
    public class DataInitializer : IDataInitializer
    {
        private readonly StoreDbContext _context;

        public DataInitializer(StoreDbContext context)
        {
            _context = context;
        }
        public async Task InitializeAsync()
        {

            try
            {
                var HasProduct = await _context.Products.AnyAsync();
                var HasBrand = await _context.ProductBrands.AnyAsync();
                var HasProductTypes = await _context.ProductTypes.AnyAsync();


                if (HasProduct && HasBrand && HasProductTypes) return;

                if (!HasBrand)
                {
                    await SeedDataFromJsonAsync<ProductBrand, int>("brands.json" , _context.ProductBrands);
                }
                if (!HasProductTypes)
                {
                  await  SeedDataFromJsonAsync<ProductType, int>("types.json", _context.ProductTypes);
                    await _context.SaveChangesAsync();

                }

                if (!HasProduct)
                {
                   await SeedDataFromJsonAsync<Product, int>("products.json", _context.Products);
                   await _context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data Sedding Failed {ex}");
            }

        }

        private async Task SeedDataFromJsonAsync<T, Tkey>(string FileName, DbSet<T> dbset) where T : BaseEntity<Tkey>
        {
            var FilePath = @"..\E Commerce.Persistence\Data\DataSeeding\JSONFiles\" + FileName;

            if(!File.Exists(FilePath)) throw new FileNotFoundException($"File:{FileName} is not Exist");

            try
            {
                using var DataStream = File.OpenRead(FilePath);

                var Data =await JsonSerializer.DeserializeAsync<List<T>>(DataStream , new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
            
            if(Data is not null)

                {
                    dbset.AddRange(Data);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Reading File : {ex}");
                throw;
            }

        }
    }
}
