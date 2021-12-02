using AutoMapper;
using biz.conevento.Entities;
using System.Collections.Generic;
using api.conevento.Models.User;

namespace api.premier.Mapper
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
