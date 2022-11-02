using Application.Loggers;
using Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Loggers.UseCaseLoggers
{
    public class ConsoleUseCaseLogger : IUseCaseLogger
    {
        public void Log(UseCaseLog log)
        {
            Console.WriteLine($"UseCase: {log.UseCaseName}, User: {log.UserIdentity}, ExecutionDateTime: {log.ExecutionDateTime}, IsAuthorized: {log.IsAuthorized}, Data: {log.Data}");
        }
    }
}
