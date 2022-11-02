using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public abstract class GlobalException : Exception
    {
        public GlobalException(string message)
            : base(message)
        {
        }

        public abstract int StatusCode { get; }
    }
}
