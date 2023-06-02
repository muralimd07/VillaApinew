using AutoMapper;
using VillaMvc.Modal;

namespace VillaMvc
{
    public class MapperConfig:Profile
    {
        public MapperConfig() {

            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberCreateDto>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDto>().ReverseMap();

        }
    }
}
