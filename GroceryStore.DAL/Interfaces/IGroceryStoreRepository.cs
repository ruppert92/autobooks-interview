using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GroceryStore.DAL.Models;

namespace GroceryStore.DAL.Interfaces
{
    public interface IGroceryStoreRepository<T> where T: BaseEntity
    {
        Task<T> GetById(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);

        Task<T> Add(T entity, CancellationToken cancellationToken = default);

        Task Update(T entity, CancellationToken cancellationToken = default);
    }
}
