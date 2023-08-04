using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickStars.MVApi.Business.Dtos;
using QuickStars.MVApi.Business.Interfaces;
using QuickStars.MVApi.Extensions;

namespace QuickStars.MVApi.Controllers
{
    [AllowAnonymous]
    //[Authorize(Roles = IdentityRoles.User)]
    [Route("api/[controller]")]
    [ApiController]
    public class LogMessageController : ControllerBase
    {
        private readonly ILogMessageService _logMessageService;

        public LogMessageController(ILogMessageService logMessageService)
        {
            _logMessageService = logMessageService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogMessageDto>>> GetAll()
        {
            var logMessages = await _logMessageService.GetLogMessages();
            return this.FromResult(logMessages);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<LogMessageDto>> GetById(long id)
        {
            var logMessage = await _logMessageService.GetLogMessageById(id);
            return this.FromResult(logMessage);
        }

        [HttpPost]
        public async Task<ActionResult<LogMessageDto>> Create([FromBody] CreateOrUpdateLogMessageDto logMessageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var logMessage = await _logMessageService.AddLogMessage(logMessageDto);
            return this.FromResult(logMessage);
        }

        [HttpPut]
        public async Task<ActionResult<LogMessageDto>> Update([FromQuery] long id, [FromBody] CreateOrUpdateLogMessageDto logMessageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var logMessage = await _logMessageService.UpdateLogMessage(id, logMessageDto);
            return this.FromResult(logMessage);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] long id)
        {
            var result = await _logMessageService.DeleteLogMessage(id);
            return this.FromResult(result);
        }

    }
}