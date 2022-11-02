using Application.DTOs;
using FluentAssertions;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class InMemoryDatabaseTests : IClassFixture<DbContextFixture>
    {
        private readonly DbContextFixture _fixture;

        public InMemoryDatabaseTests(DbContextFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData("pera@gmail.com")]
        [InlineData("laza@gmail.com")]
        public void ReturnsError_WhenEmailIsNotUnique(string email)
        {
            var validator = new RegisterUserValidator(_fixture.InMemoryContext);

            var dto = new RegisterUserDto
            {
                FirstName = "Firstname",
                LastName = "Lastname",
                Username = "username",
                Email = email,
                Password = "Sifra123!"
            };

            var result = validator.Validate(dto);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(1);
            result.Errors.First().ErrorMessage.Should().Contain("already exists");
        }
    }
}
