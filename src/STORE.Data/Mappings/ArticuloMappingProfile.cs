using AutoMapper;
using STORE.Entities.Dtos.Articulo;
using STORE.Entities.Models;

namespace STORE.Data.Mappings
{
    public class ArticuloMappingProfile : Profile
    {
        public ArticuloMappingProfile()
        {
            CreateMap<Articulo, ListArticulosDto>()
                .ReverseMap();

            CreateMap<Articulo, ArticuloCreateDto>()
                .ReverseMap();
        }
    }
}
