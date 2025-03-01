using AutoMapper;
using VillaWeb.Models;

namespace VillaWeb
{
    public class MapperConfig:Profile
    {
        public MapperConfig() {

            CreateMap<Villa, VillaDto>();
            CreateMap<VillaDto, Villa>();
            CreateMap<VillaDto, VillaCreateDto>().ReverseMap();
            CreateMap<VillaDto, VillaUpdateDto>().ReverseMap();


            CreateMap<VillaNumber, VillaNumberDto>();
            CreateMap<VillaNumberDto, VillaNumber>();
            CreateMap<VillaNumber, VillaNumberCreateDto>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDto>().ReverseMap();

        }
    }
}
