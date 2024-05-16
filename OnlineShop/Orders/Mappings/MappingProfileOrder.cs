using AutoMapper;
using OnlineShop.OrderDetails.Models;
using OnlineShop.Orders.Dto;

namespace OnlineShop.Orders.Mappings
{
    public class MappingProfileOrder : Profile
    {
        public MappingProfileOrder() {
            CreateMap<CreateRequestOrder, Order>();
        }
    }
}
