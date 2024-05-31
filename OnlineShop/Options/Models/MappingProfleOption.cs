using AutoMapper;
using OnlineShop.Options.Dto;
using OnlineShop.Options.Models;
using OnlineShop.ProductOptions.Model;

namespace OnlineShop.Options.Mappings
{
    public class MappingProfleOption : Profile
    {
        public MappingProfleOption()
        {
            CreateMap<CreateRequestOption, Option>();
            CreateMap<OptionResponse, Option>();
            CreateMap<ProductOption, OptionResponse>().ForPath(s=>s.Name,op=>op.MapFrom(s=>s.Option.Name))
                .ForPath(s => s.Price, op => op.MapFrom(s => s.Option.Price));
          //  CreateMap<Option,ProductOption>().ReverseMap(); 

        }

    }
}
