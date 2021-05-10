using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GroceryStore.DAL.Models;

namespace GroceryStore.DAL.Contexts
{
    public interface IGroceryStoreDbContext
    {
        Task<T> GetById<T>(int id, CancellationToken cancellationToken = default) where T : BaseEntity;

        Task<IEnumerable<T>> GetAll<T>(CancellationToken cancellationToken = default);

        Task<T> Add<T>(T entity, CancellationToken cancellationToken = default) where T : BaseEntity;

        Task Update<T>(T entity, CancellationToken cancellationToken = default) where T : BaseEntity;
    }
}
