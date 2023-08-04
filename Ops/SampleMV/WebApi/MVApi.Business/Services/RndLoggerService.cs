using Microsoft.Extensions.Logging;
using QuickStars.MVApi.Business.Interfaces;
using QuickStars.MVApi.Domain.Interfaces;

namespace QuickStars.MVApi.Business.Services
{
    public class RndLoggerService : IRndLoggerService
    {
        private readonly ILogger<RndLoggerService> _logger;
        //public readonly ILogMessageRepository _logMessageRepository;

        public RndLoggerService(ILogger<RndLoggerService> logger)//, ILogMessageRepository logMessageRepository)
        {
            _logger = logger;
            //_logMessageRepository = logMessageRepository;
        }

        public Task GenerateRandomLogs()
        {
            var random = new Random();

            int nbLogs = random.Next(1, 6);
            for (int i = 0; i < nbLogs; i++)
            {
                var logLevel = GetRandomLogLevel(random);
                var message = GetRandomMessage(random);
                _logger.Log(logLevel, message);
            }
            return Task.CompletedTask;
        }

        private LogLevel GetRandomLogLevel(Random random)
        {
            var logLevels = Enum.GetValues(typeof(LogLevel));
            var randomLevel = logLevels.GetValue(random.Next(logLevels.Length));
            if (randomLevel is null) return LogLevel.Information;
            return (LogLevel)randomLevel;
        }

        private string GetRandomMessage(Random random)
        {
            var messages = new[]
            {
                "Hello from Log.IO!",
                "Application is running",
                "Error occurred in module A!",
                "418: This is a teapot",
                "Knock, knock. Who's there? NullPointerException.",
                "I don't always test my code, but when I do, I do it in production.",
                "Roses are red, violets are blue, unexpected '{' on line 32.",
                "The best thing about a boolean is even if you are wrong, you are only off by a bit.",
                "Houston, we have a log.",
                "Log me if you can.",
                "Log. James Log.",
                "With great logs comes great responsibility.",
                "Are you not entertained by these logs?",
                "One does not simply log into Mordor.",
                "May the log be with you!",
            };
            return messages[random.Next(messages.Length)];
        }

    }
}