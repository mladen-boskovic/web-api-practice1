using EasyNetQ;
using MessageBus.Messages;

namespace MailingService.Core
{
    public static class StartupManager
    {
        public static void Subscribe(IBus messageBus)
        {
            messageBus.PubSub.Subscribe<UserHasRegistered>("MailingService.UserHasRegistered", SendEmail);
        }

        private static void SendEmail(UserHasRegistered x)
        {
            Console.WriteLine($"User {x.Email} has registered. Sending email...");
        }
    }
}
