using DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases
{
    public abstract class EfUseCase
    {
        protected readonly WebApiPracticeContext Context;
        protected EfUseCase(WebApiPracticeContext context)
        {
            Context = context;
        }
    }
}
