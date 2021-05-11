using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GroceryStore.BLL.DTOs;

namespace GroceryStore.BLL.Interfaces
{
    /// <summary>
    /// Service for interacting with Grocery Store Customers
    /// </summary>
    public interface ICustomersService
    {
        /// <summary>
        /// Gets customer by id
        /// </summary>
        /// <param name="id">Id of customer</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Customer</returns>
        Task<CustomerDTO> GetById(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all customers from grocery store
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Enumerable of all customers</returns>
        Task<IEnumerable<CustomerDTO>> GetAll(CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a customer to the grocery store
        /// </summary>
        /// <param name="entity">Customer to add</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Added customer</returns>
        Task<CustomerDTO> Add(CustomerDTO entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates a customer's information
        /// </summary>
        /// <param name="entity">Updated customer information</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task</returns>
        Task Update(CustomerDTO entity, CancellationToken cancellationToken = default);
    }
}
