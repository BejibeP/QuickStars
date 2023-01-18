using Microsoft.Extensions.Logging;

using ILoggerFactory loggerFactory = LoggerFactory.Create(builder => 
    builder.AddSimpleConsole(options =>
    {
        options.IncludeScopes = true;
        options.SingleLine = true;
        options.TimestampFormat = "HH:mm:ss ";
    }));

ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

logger.LogInformation("Hello World!");
logger.LogInformation("Logs contain timestamp and log level.");
logger.LogInformation("Each log message is fit in a single line.");

while (true)
{
    var random = new Random();

    int nbLogs = random.Next(1,6);
    for(int i = 0; i < nbLogs; i++) 
    {

        var logLevel = GetRandomLogLevel(random);
        var message = GetRandomMessage(random);
        logger.Log(logLevel, message);

    }

    int nextTimer = random.Next(10000,60000); //30000; // timer in ms (more than 1s less than 60s)
    System.Threading.Thread.Sleep(nextTimer);
}

static LogLevel GetRandomLogLevel(Random random)
{
    var logLevels = Enum.GetValues(typeof(LogLevel));
    var randomLevel = logLevels.GetValue(random.Next(logLevels.Length));
    if(randomLevel is null) return LogLevel.Information;
    return (LogLevel)randomLevel;
}

static string GetRandomMessage(Random random)
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
 