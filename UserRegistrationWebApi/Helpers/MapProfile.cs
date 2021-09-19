using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Entities;
using UserRegistrationWebApi.DTOs;

namespace UserRegistrationWebApi.Helpers
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<User,UserDto>();
            CreateMap<UserDto, User>();

        }

    }

}
