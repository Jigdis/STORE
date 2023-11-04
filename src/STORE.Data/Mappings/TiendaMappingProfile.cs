using AutoMapper;
using STORE.Data.Dtos.Tienda;
using STORE.Entities.Models;

namespace STORE.Data.Mappings
{
    public class TiendaMappingProfile : Profile
    {
        public TiendaMappingProfile()
        {
            CreateMap<Tienda, TiendaDto>()
                .ReverseMap();

            CreateMap<Tienda, TiendaCreateDto>()
                .ReverseMap();
        }
    }
}
