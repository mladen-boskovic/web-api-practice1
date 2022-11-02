using Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class EntityNotFoundException : GlobalException
    {
        public EntityNotFoundException(string data)
            : base($"Entity not found. {data}")
        {
        }

        public override int StatusCode => 404;
    }
}
