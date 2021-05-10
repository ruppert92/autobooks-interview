using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GroceryStore.BLL.DTOs;

namespace GroceryStore.BLL.Interfaces
{
    public interface ICustomersService
    {
        Task<CustomerDTO> GetById(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<CustomerDTO>> GetAll(CancellationToken cancellationToken = default);

        Task<CustomerDTO> Add(CustomerDTO entity, CancellationToken cancellationToken = default);

        Task Update(CustomerDTO entity, CancellationToken cancellationToken = default);
    }
}
