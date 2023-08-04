using Microsoft.Extensions.Options;
using QuickStars.MVApi.Business.Interfaces;
using QuickStars.MVApi.Domain.Configuration;

namespace QuickStars.MVApi.Business.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly AppSettings _appSettings;
        private readonly ExternalApiSettings _apiSettings;
        private readonly DatabaseSettings _databaseSettings;
        private readonly MessagingSettings _messagingSettings;
        private readonly LoggingSettings _loggingSettings;

        public ConfigurationService(IOptions<AppSettings> appSettings, IOptions<ExternalApiSettings> apiSettings,
            IOptions<DatabaseSettings> databaseSettings, IOptions<MessagingSettings> messagingSettings, IOptions<LoggingSettings> loggingSettings)
        {
            _appSettings = appSettings.Value;
            _apiSettings = apiSettings.Value;
            _databaseSettings = databaseSettings.Value;
            _messagingSettings = messagingSettings.Value;
            _loggingSettings = loggingSettings.Value;
        }

        public Task<AppSettings> GetAppSettings()
        {
            return Task.FromResult(_appSettings);
        }

        public Task<ExternalApiSettings> GetApiSettings()
        {
            return Task.FromResult(_apiSettings);
        }

        public Task<DatabaseSettings> GetDatabaseSettings()
        {
            return Task.FromResult(_databaseSettings);
        }

        public Task<MessagingSettings> GetMessagingSettings()
        {
            return Task.FromResult(_messagingSettings);
        }

        public Task<LoggingSettings> GetLoggingSettings()
        {
            return Task.FromResult(_loggingSettings);
        }
    }
}