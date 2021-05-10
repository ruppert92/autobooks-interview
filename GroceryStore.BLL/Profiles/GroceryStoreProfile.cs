using AutoMapper;
using GroceryStore.BLL.DTOs;
using GroceryStore.DAL.Models;

namespace GroceryStore.BLL.Profiles
{
    public class GroceryStoreProfile : Profile
    {
        public GroceryStoreProfile()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
        }
    }
}
