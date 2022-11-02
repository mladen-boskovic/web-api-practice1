using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangfire.Core
{
    public class ApplicationSettings
    {
        public string HangfireConnection { get; set; }
        public string ConnectionString { get; set; }
    }
}
