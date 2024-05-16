using AutoMapper;
using OnlineShop.Options.Dto;
using OnlineShop.Options.Models;

namespace OnlineShop.Options.Mappings
{
    public class MappingProfleOption : Profile
    {
        public MappingProfleOption()
        {
            CreateMap<CreateRequestOption, Option>();
        }

    }
}
