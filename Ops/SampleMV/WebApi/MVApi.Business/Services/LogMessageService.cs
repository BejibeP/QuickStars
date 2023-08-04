using QuickStars.MVApi.Business.Dtos;
using QuickStars.MVApi.Business.Dtos.Extensions;
using QuickStars.MVApi.Business.Interfaces;
using QuickStars.MVApi.Domain.Interfaces;

namespace QuickStars.MVApi.Business.Services
{
    public class LogMessageService : ILogMessageService
    {
        public readonly ILogMessageRepository _logMessageRepository;

        public LogMessageService(ILogMessageRepository logMessageRepository)
        {
            _logMessageRepository = logMessageRepository;
        }

        public async Task<ServiceResult<IEnumerable<LogMessageDto>>> GetLogMessages()
        {
            var logMessages = await _logMessageRepository.GetAll();

            var logMessageDtos = logMessages.Select(e => e.ToDto()).ToList();

            return ServiceResult<IEnumerable<LogMessageDto>>.Success(logMessageDtos);
        }

        public async Task<ServiceResult<LogMessageDto>> GetLogMessageById(long id)
        {
            var logMessage = await _logMessageRepository.GetById(id);
            if (logMessage is null)
                return ServiceResult<LogMessageDto>.NotFound("LogMessage not found");

            return ServiceResult<LogMessageDto>.Success(logMessage.ToDto());
        }

        public async Task<ServiceResult<LogMessageDto>> AddLogMessage(CreateOrUpdateLogMessageDto dto)
        {
            var logMessage = await _logMessageRepository.Create(dto.ToLogMessage());
            if (logMessage is null)
                return ServiceResult<LogMessageDto>.ServerError();

            return ServiceResult<LogMessageDto>.Success(logMessage.ToDto());
        }

        public async Task<ServiceResult<LogMessageDto>> UpdateLogMessage(long id, CreateOrUpdateLogMessageDto dto)
        {
            var logMessage = await _logMessageRepository.GetById(id, true);
            if (logMessage == null)
                return ServiceResult<LogMessageDto>.NotFound("LogMessage not found");

            logMessage = await _logMessageRepository.Update(logMessage.UpdateLogMessage(dto));
            if (logMessage is null)
                return ServiceResult<LogMessageDto>.ServerError();

            return ServiceResult<LogMessageDto>.Success(logMessage.ToDto());
        }

        public async Task<ServiceResult<bool>> DeleteLogMessage(long id)
        {
            bool result = await _logMessageRepository.Delete(id);
            if (!result)
                return ServiceResult<bool>.NotFound();

            return ServiceResult<bool>.NoContent();
        }

    }
}