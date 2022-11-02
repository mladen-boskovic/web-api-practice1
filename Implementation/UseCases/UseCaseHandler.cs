using Application.Exceptions;
using Application.Loggers;
using Application.UseCases;
using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases
{
    public class UseCaseHandler
    {
        private readonly IApplicationUser _user;
        private readonly IUseCaseLogger _useCaseLogger;

        public UseCaseHandler(IApplicationUser user, IUseCaseLogger useCaseLogger)
        {
            _user = user;
            _useCaseLogger = useCaseLogger;
        }

        public void HandleCommand<TRequest>(ICommand<TRequest> command, TRequest request)
        {
            try
            {
                HandleLoggingAndAuthorization(command, request);

                var stopwatch = new Stopwatch();
                stopwatch.Start();
                command.Execute(request);
                stopwatch.Stop();

                Console.WriteLine($"{command.Name}: {stopwatch.ElapsedMilliseconds} ms.");
            }
            catch(Exception e)
            {
                throw;
            }
        }

        public TResult HandleQuery<TRequest, TResult>(IQuery<TRequest, TResult> query, TRequest request)
        {
            try
            {
                HandleLoggingAndAuthorization(query, request);

                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var result = query.Execute(request);
                stopwatch.Stop();

                Console.WriteLine($"{query.Name}: {stopwatch.ElapsedMilliseconds} ms.");

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private void HandleLoggingAndAuthorization<TRequest>(IUseCase useCase, TRequest request)
        {
            var isAuthorized = _user.UseCaseIds.Contains(useCase.Id);

            var log = new UseCaseLog()
            {
                UseCaseName = useCase.Name,
                UserIdentity = _user.Identity,
                UserId = _user.Id,
                ExecutionDateTime = DateTime.Now,
                Data = JsonConvert.SerializeObject(request),
                IsAuthorized = isAuthorized
            };

            _useCaseLogger.Log(log);

            if (!isAuthorized)
            {
                throw new ForbiddenUseCaseExecutionException($"User: {_user.Identity}, UseCase: {useCase.Name}");
            }
        }
    }
}
