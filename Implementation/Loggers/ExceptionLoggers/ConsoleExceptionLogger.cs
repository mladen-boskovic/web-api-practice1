using Application.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Loggers.ExceptionLoggers
{
    public class ConsoleExceptionLogger : IExceptionLogger
    {
        public void Log(Exception e)
        {
            Console.WriteLine($"Exception occured. DateTime: {DateTime.Now}, Message: {e.Message}");
        }
    }
}
