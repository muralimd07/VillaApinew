using AutoMapper;
using VillaApinew.Modal;

namespace VillaApinew.Mapper
{
    public class MapperConfig:Profile
    {
        public MapperConfig() {

            CreateMap<Villa, VillaDto>();
            CreateMap<VillaDto, Villa>();
            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();


            CreateMap<VillaNumber, VillaNumberDto>();
            CreateMap<VillaNumberDto, VillaNumber>();
            CreateMap<VillaNumber, VillaNumberCreateDto>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDto>().ReverseMap();

        }
    }
}
