using GroceryStore.DAL.Contexts;
using GroceryStore.DAL.Interfaces;
using GroceryStore.DAL.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GroceryStore.DAL.Repositories
{
    /// <inheritdoc />
    public class GroceryStoryRepository<T> : IGroceryStoreRepository<T> where T : BaseEntity
    {
        private readonly IGroceryStoreDbContext _dbContext;
        public GroceryStoryRepository (IGroceryStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<T> GetById(int id, CancellationToken cancellationToken = default) 
        {
            return await _dbContext.GetById<T>(id, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default)
        {
            return await _dbContext.GetAll<T>(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<T> Add(T entity, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Add(entity, cancellationToken);
        }

        /// <inheritdoc />
        public async Task Update(T entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Update(entity, cancellationToken);
        }
    }
}
