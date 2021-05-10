using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GroceryStore.BLL.DTOs;
using GroceryStore.BLL.Interfaces;
using GroceryStore.DAL.Interfaces;
using GroceryStore.DAL.Models;

namespace GroceryStore.BLL.Services
{
    public class CustomersService: ICustomersService
    {
        private readonly IGroceryStoreRepository<Customer> _customersRepository;
        private readonly IMapper _mapper;

        public CustomersService(IGroceryStoreRepository<Customer> customersRepository, IMapper mapper)
        {
            _customersRepository = customersRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDTO> GetById(int id, CancellationToken cancellationToken = default)
        {
            var customer = await _customersRepository.GetById(id, cancellationToken);
            return _mapper.Map<CustomerDTO>(customer);
        }

        public async Task<IEnumerable<CustomerDTO>> GetAll(CancellationToken cancellationToken = default)
        {
            var customers = await _customersRepository.GetAll(cancellationToken);
            return _mapper.Map<IEnumerable<CustomerDTO>>(customers);
        }

        public async Task<CustomerDTO> Add(CustomerDTO entityDto, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<Customer>(entityDto);
            var addedEntity = await _customersRepository.Add(entity, cancellationToken);
            return _mapper.Map<CustomerDTO>(addedEntity);
        }

        public async Task Update(CustomerDTO entityDto, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<Customer>(entityDto);
            await _customersRepository.Update(entity, cancellationToken);
        }
    }
}
