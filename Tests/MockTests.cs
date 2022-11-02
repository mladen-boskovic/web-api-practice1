using Application.DTOs;
using Application.UseCases.Queries;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class MockTests
    {
        [Fact]
        public void GetUser()
        {
            var mock = new Mock<IGetUserQuery>();

            mock.Setup(x => x.Execute("peraperic")).Returns(new UserDto
            {
                Username = "peraperic"
            });

            var instance = mock.Object;
            var user = instance.Execute("peraperic");
            user.Username.Should().Be("peraperic");
        }
    }
}
