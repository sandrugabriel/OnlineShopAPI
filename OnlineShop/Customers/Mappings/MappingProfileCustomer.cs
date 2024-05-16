using AutoMapper;
using OnlineShop.Customers.Dto;
using OnlineShop.Customers.Models;

namespace OnlineShop.Customers.Mappings
{
    public class MappingProfileCustomer : Profile
    {
        public MappingProfileCustomer()
        {
            CreateMap<CreateRequestCustomer, Customer>();
        }
    }
}
