using Application.Loggers;
using Bugsnag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Loggers.ExceptionLoggers
{
    public class BugsnagExceptionLogger : IExceptionLogger
    {
        private readonly Bugsnag.IClient _bugsnagClient;

        public BugsnagExceptionLogger(IClient bugsnagClient)
        {
            _bugsnagClient = bugsnagClient;
        }

        public void Log(Exception e)
        {
            _bugsnagClient.Notify(e);
        }
    }
}
