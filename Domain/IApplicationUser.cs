using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IApplicationUser
    {
        int Id { get; }
        string Identity { get; }
        string Email { get; }
        IEnumerable<int> UseCaseIds { get; }
    }
}
