using AutoMapper;
using OnlineShop.Products.Models;
using OnlineShop.Products.Dto;


namespace OnlineShop.Products.Mappings
{
    public class MappingProfilesProduct : Profile
    {
        public MappingProfilesProduct() {

            CreateMap<CreateRequestProduct, Product>();
        }
    }
}
