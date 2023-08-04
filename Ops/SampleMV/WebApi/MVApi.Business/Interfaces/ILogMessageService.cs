using QuickStars.MVApi.Business.Dtos;

namespace QuickStars.MVApi.Business.Interfaces
{
    public interface ILogMessageService
    {
        Task<ServiceResult<IEnumerable<LogMessageDto>>> GetLogMessages();

        Task<ServiceResult<LogMessageDto>> GetLogMessageById(long id);

        Task<ServiceResult<LogMessageDto>> AddLogMessage(CreateOrUpdateLogMessageDto logMessageDto);

        Task<ServiceResult<LogMessageDto>> UpdateLogMessage(long id, CreateOrUpdateLogMessageDto logMessageDto);

        Task<ServiceResult<bool>> DeleteLogMessage(long id);
    }
}