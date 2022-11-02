using Application.DTOs;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dto => dto.UseCaseIds, opt => opt.MapFrom(user => user.UserUseCases.Select(uuc => uuc.UseCaseId)));
            CreateMap<UserDto, User>();
        }
    }
}
