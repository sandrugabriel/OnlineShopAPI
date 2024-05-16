using AutoMapper;
using OnlineShop.ProductOptions.Dto;
using OnlineShop.ProductOptions.Model;

namespace OnlineShop.ProductOptions.Mappings
{
    public class MappingProfileProductOption : Profile
    {
        public MappingProfileProductOption() {
            CreateMap<CreateRequestProductOption, ProductOption>();
        }
    }
}
