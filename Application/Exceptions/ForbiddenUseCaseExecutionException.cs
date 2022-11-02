using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ForbiddenUseCaseExecutionException : GlobalException
    {
        public ForbiddenUseCaseExecutionException(string data)
            : base($"Forbidden UseCase execution. {data}")
        {
        }

        public override int StatusCode => 403;
    }
}
