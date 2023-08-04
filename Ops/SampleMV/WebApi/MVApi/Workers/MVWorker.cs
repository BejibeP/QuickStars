using QuickStars.MVApi.Business.Interfaces;

namespace QuickStars.MVApi.Workers
{
    public class MVWorker : BackgroundService
    {
        private readonly ILogger<MVWorker> _logger;
        private readonly IRndLoggerService _rndLoggerService;

        private int _executionCount;

        public MVWorker(ILogger<MVWorker> logger, IRndLoggerService rndLoggerService)
        {
            _logger = logger;
            _rndLoggerService = rndLoggerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            using PeriodicTimer timer = new(TimeSpan.FromSeconds(45));
            try
            {
                do
                {
                    await DoWork();
                }
                while (await timer.WaitForNextTickAsync(stoppingToken));

            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Timed Hosted Service is stopping.");
            }
        }

        private async Task DoWork()
        {
            if (_rndLoggerService == null) throw new OperationCanceledException();

            await _rndLoggerService.GenerateRandomLogs();

            int count = Interlocked.Increment(ref _executionCount);
            _logger.LogInformation("Timed Hosted Service is working. Count: {Count}", count);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

    }
}