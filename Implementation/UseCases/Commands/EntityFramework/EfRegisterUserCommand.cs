using Application.DTOs;
using Application.UseCases.Commands;
using DataAccess.EntityFramework;
using Domain;
using EasyNetQ;
using FluentValidation;
using Implementation.Validators;
using MessageBus.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.EntityFramework
{
    public class EfRegisterUserCommand : EfUseCase, IRegisterUserCommand
    {
        private readonly RegisterUserValidator _validator;
        private readonly IBus _messageBus;

        public EfRegisterUserCommand(WebApiPracticeContext context, RegisterUserValidator validator, IBus messageBus) : base(context)
        {
            _validator = validator;
            _messageBus = messageBus;
        }

        public int Id => 1;

        public string Name => "Register User using Entity Framework";

        public void Execute(RegisterUserDto request)
        {
            _validator.ValidateAndThrow(request);

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = passwordHash,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserUseCases = new List<UserUseCase>
                {
                    new UserUseCase
                    {
                        UseCaseId = 3
                    }
                }
            };

            Context.Add(user);
            Context.SaveChanges();

            _messageBus.PubSub.Publish(new UserHasRegistered
            {
                Email = request.Email
            });
        }
    }
}
