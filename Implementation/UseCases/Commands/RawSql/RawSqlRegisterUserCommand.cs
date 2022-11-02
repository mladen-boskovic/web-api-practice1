using Application.DTOs;
using Application.UseCases.Commands;
using EasyNetQ;
using FluentValidation;
using Implementation.Validators;
using MessageBus.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases.Commands.RawSql
{
    public class RawSqlRegisterUserCommand : IRegisterUserCommand
    {
        private readonly IDbConnection _dbConnection;
        private readonly RegisterUserValidator _validator;
        private readonly IBus _messageBus;

        public RawSqlRegisterUserCommand(IDbConnection dbConnection, RegisterUserValidator validator, IBus messageBus)
        {
            _dbConnection = dbConnection;
            _validator = validator;
            _messageBus = messageBus;
        }

        public int Id => 2;

        public string Name => "Register User using Raw Sql";

        public void Execute(RegisterUserDto request)
        {
            _validator.ValidateAndThrow(request);

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var query = "INSERT INTO Users (FirstName, LastName, Username, Email, Password) VALUES" +
                        $"('{request.FirstName}', '{request.LastName}', '{request.Username}', '{request.Email}', '{passwordHash}');";

            var command = _dbConnection.CreateCommand();
            command.CommandText = query;
            _dbConnection.Open();
            command.ExecuteNonQuery();

            query = $"SELECT Id FROM Users WHERE Username='{request.Username}'";
            command.CommandText = query;
            var reader = command.ExecuteReader();
            var userId = 0;

            while (reader.Read())
            {
                userId = reader.GetInt32(0);
            }

            query = $"INSERT INTO UserUseCases (UserId, UseCaseId) VALUES ({userId}, 3)";
            command.CommandText = query;
            _dbConnection.Close();
            _dbConnection.Open();
            command.ExecuteNonQuery();
            _dbConnection.Close();

            _messageBus.PubSub.Publish(new UserHasRegistered
            {
                Email = request.Email
            });
        }
    }
}
