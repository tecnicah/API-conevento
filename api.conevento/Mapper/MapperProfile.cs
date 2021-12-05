using AutoMapper;
using api.conevento.Models.User;
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

        }
    }
}
