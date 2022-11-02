using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBus.Messages
{
    public class UserHasRegistered
    {
        public string Email { get; set; }
    }
}
