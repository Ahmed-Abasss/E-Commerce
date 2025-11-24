using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistence.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;

        private Dictionary<Type, object> _repositories = [];
        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            var entityType = typeof(TEntity);

            if (_repositories.TryGetValue(entityType, out var repository))
                return(IGenericRepository<TEntity, Tkey>) repository;

            var newRepo = new GenericRepository<TEntity, Tkey>(_dbContext);
            _repositories[entityType] = newRepo;

            return newRepo;
        }

        public async Task SaveChangesAsync()=>await _dbContext.SaveChangesAsync();
     
    }
}
