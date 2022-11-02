using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class UseCaseConflictException : GlobalException
    {
        public UseCaseConflictException(string data)
            : base($"UseCase conflict. {data}")
        {
        }

        public override int StatusCode => 409;
    }
}
