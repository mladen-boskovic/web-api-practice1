using EasyNetQ;
using MailingService.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient(x => RabbitHutch.CreateBus("host=localhost"));

var app = builder.Build();

var messageBus = app.Services.GetService<IBus>();

StartupManager.Subscribe(messageBus);

app.Run();
