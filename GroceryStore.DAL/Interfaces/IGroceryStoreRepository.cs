using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GroceryStore.DAL.Models;

namespace GroceryStore.DAL.Interfaces
{
    /// <summary>
    /// Generic repository for accessing grocery store data
    /// </summary>
    /// <typeparam name="T">Type of entity in data store</typeparam>
    public interface IGroceryStoreRepository<T> where T: BaseEntity
    {
        /// <summary>
        /// Gets an entity by id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The matching entity or null</returns>
        Task<T> GetById(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all of the entites
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Enumerable of all entities</returns>
        Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds entity to data store
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Added entity</returns>
        Task<T> Add(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates entity in data store
        /// </summary>
        /// <param name="entity">Updated entity</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task</returns>
        Task Update(T entity, CancellationToken cancellationToken = default);
    }
}
