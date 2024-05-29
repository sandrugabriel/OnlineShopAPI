using AutoMapper;
using OnlineShop.OrderDetails.Models;
using OnlineShop.Orders.Dto;

namespace OnlineShop.Orders.Mappings
{
    public class MappingProfileOrder : Profile
    {
        public MappingProfileOrder() {
            CreateMap<CreateRequestOrder, Order>();
            CreateMap<Order, DtoOrderView>().ForMember(s => s.Products, op => op.MapFrom(sr => sr.OrderDetails));
        }
    }
}
