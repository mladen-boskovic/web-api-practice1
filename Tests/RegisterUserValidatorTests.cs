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
    public class RegisterUserValidatorTests : IClassFixture<DbContextFixture>
    {
        private readonly DbContextFixture _fixture;

        public RegisterUserValidatorTests(DbContextFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ReturnsError_WhenDataIsNotProvided()
        {
            var validator = new RegisterUserValidator(_fixture.Context);

            var dto = new RegisterUserDto
            {
                FirstName = "",
                LastName = "",
                Username = "",
                Email = "",
                Password = ""
            };

            var result = validator.Validate(dto);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(5);
        }
    }
}
