using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UseCase : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<UserUseCase> UserUseCases { get; set; }
    }
}
