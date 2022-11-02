# web-api-practice1
C# / .NET 6 / Web API / JWT / Hangfire / Bugsnag / MessageBus

In order to use application at localhost we need to:
1. Attach WebApiPractice database from folder Database to SSMS
2. Create empty database called Hangfire (once Hangfire project is started, database will populate)
3. Connect to both databases in Visual Studio
4. Copy and replace connection string of WebApiPractice database to appsettings.Development.json file in following projects: API, Hangfire, Tests
5. Copy and replace connection string of Hangfire database to appsettings.Development.json file in Hangfire project

Software requirements:
1. .NET 6 - SDK & ASP.NET Core Runtime (https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
2. Erlang (https://erlang.org/download/otp_versions_tree.html)
3. RabbitMQ (https://www.rabbitmq.com/install-windows.html)
After installing RabbitMQ we need to enable Management Plugin. Find installation folder of RabbitMQ an then go to sbin folder. Open CMD and execute: "rabbitmq-plugins enable rabbitmq_management"

How to use application (all functionalities):
- Open solution in Visual Studio and build solution. Right click on solution -> Properties -> and set Multiple startup projects (API, Hangfire, MailingService). Start application
- On http://localhost:5000/hangfire we can find Hangfire Dashboard with available jobs
- MailingService console will inform us if user has registered. Also on http://localhost:15672/ we can find GUI. Credentials are guest-guest

- API will start in console and browser (Swagger). In console we can see executed UseCases and via Swagger we can register user, authorize and get user's information:
1. Register user with valid data (/api/Register)
2. Get token using email and password (/api/Token)
3. Authorize using token (Authorize button -> Bearer [token] -> Authorize)
4. Get user's information (authorization is required)

We can monitor all application exceptions on https://www.bugsnag.com/ (contact creator for credentials)
