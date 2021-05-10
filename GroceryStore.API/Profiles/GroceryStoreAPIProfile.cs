using AutoMapper;
using GroceryStore.API.Models;
using GroceryStore.BLL.DTOs;

namespace GroceryStore.API.Profiles
{
    public class GroceryStoreAPIProfile : Profile
    {
        public GroceryStoreAPIProfile()
        {
            CreateMap<AddCustomerRequest, CustomerDTO>();
        }
    }
}
