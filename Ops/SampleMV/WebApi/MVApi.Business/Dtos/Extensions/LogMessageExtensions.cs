using QuickStars.MVApi.Domain.Data.Entities;

namespace QuickStars.MVApi.Business.Dtos.Extensions
{
    public static class LogMessageExtensions
    {
        public static LogMessage ToLogMessage(this CreateOrUpdateLogMessageDto dto)
        {
            return new LogMessage()
            {
                Message = dto.Message,
                IsActive = true
            };
        }

        public static LogMessage UpdateLogMessage(this LogMessage logMessage, CreateOrUpdateLogMessageDto dto)
        {
            logMessage.Message = dto.Message;
            logMessage.IsActive = true;
            return logMessage;
        }

        public static LogMessageDto ToDto(this LogMessage logMessage)
        {
            LogMessageDto result = new LogMessageDto()
            {
                Id = logMessage.Id,
                Message = logMessage.Message,
                IsActive = logMessage.IsActive
            };
            return result;
        }
    }
}