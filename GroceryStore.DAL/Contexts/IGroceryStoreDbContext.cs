using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GroceryStore.DAL.Models;

namespace GroceryStore.DAL.Contexts
{
    /// <summary>
    /// Grocery Store Database Context
    /// </summary>
    public interface IGroceryStoreDbContext
    {
        /// <summary>
        /// Gets the requested type by id
        /// </summary>
        /// <typeparam name="T">Type of entity requesting</typeparam>
        /// <param name="id">Id of entity requesting</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The entity if found, or null</returns>
        Task<T> GetById<T>(int id, CancellationToken cancellationToken = default) where T : BaseEntity;

        /// <summary>
        /// Gets all of the entites of the requested type
        /// </summary>
        /// <typeparam name="T">Type of entity requesting</typeparam>
        /// <param name="cancellationToken"></param>
        /// <returns>An enumerable of all entites of the type requested</returns>
        Task<IEnumerable<T>> GetAll<T>(CancellationToken cancellationToken = default);

        /// <summary>
        /// Add the entity to the database
        /// </summary>
        /// <typeparam name="T">Type of entity adding</typeparam>
        /// <param name="entity">Entity adding</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The added entity (with id)</returns>
        Task<T> Add<T>(T entity, CancellationToken cancellationToken = default) where T : BaseEntity;

        /// <summary>
        /// Updates the entity in the database
        /// </summary>
        /// <typeparam name="T">Type of entity updating</typeparam>
        /// <param name="entity">Entity updating</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task</returns>
        Task Update<T>(T entity, CancellationToken cancellationToken = default) where T : BaseEntity;
    }
}
