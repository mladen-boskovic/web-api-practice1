using Application.DTOs;
using Application.Exceptions;
using Application.UseCases.Queries;
using AutoMapper;
using DataAccess.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Queries.EntityFramework
{
    public class EfGetUserQuery : EfUseCase, IGetUserQuery
    {
        private readonly IMapper _mapper;

        public EfGetUserQuery(WebApiPracticeContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public int Id => 3;

        public string Name => "Get User using Entity Framework";

        public UserDto Execute(string request)
        {
            var user = Context.Users.Include(x => x.UserUseCases).FirstOrDefault(x => x.Username == request);

            if(user == null)
            {
                throw new EntityNotFoundException($"User with username={request}");
            }

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}
