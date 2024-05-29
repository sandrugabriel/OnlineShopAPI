using AutoMapper;
using OnlineShop.OrderDetails.Dto;
using OnlineShop.OrderDetails.Models;

namespace OnlineShop.OrderDetails.Mappings
{
    public class MappingProfileOrderDetail : Profile
    {
        public MappingProfileOrderDetail() {
            CreateMap<CreateRequestOrderDetail, OrderDetail>();
            CreateMap<OrderDetail, DtoOrderDetailView>().ForMember(s => s.Product, opt => opt.MapFrom(sr => sr.Product));


        }
    }
}
