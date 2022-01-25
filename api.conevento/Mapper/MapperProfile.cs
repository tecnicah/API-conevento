using AutoMapper;
using api.conevento.Models.User;
using api.conevento.Models.Events;
using api.conevento.Models.Services;
using biz.conevento.Entities;


namespace api.conevento.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region User
            CreateMap<User, UserDto>().ReverseMap();
            #endregion
            #region Eventos
            CreateMap<Evento, EventoDto>().ReverseMap();
            CreateMap<ListaProductosEvento, ListaProductosEventoDto>().ReverseMap();
            CreateMap<CatProductosServicio, DtoCatProductosServicios>().ReverseMap();
            #endregion
        }
    }
}
