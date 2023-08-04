using QuickStars.MVApi.Domain.Configuration;

namespace QuickStars.MVApi.Business.Interfaces
{
    public interface IConfigurationService
    {
        Task<AppSettings> GetAppSettings();
        Task<ExternalApiSettings> GetApiSettings();
        Task<DatabaseSettings> GetDatabaseSettings();
        Task<MessagingSettings> GetMessagingSettings();
        Task<LoggingSettings> GetLoggingSettings();
    }
}