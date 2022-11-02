using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class CustomUnauthorizedAccessException : GlobalException
    {
        public CustomUnauthorizedAccessException(string data)
            : base($"Unauthorized access. {data}")
        {
        }

        public override int StatusCode => 401;
    }
}
