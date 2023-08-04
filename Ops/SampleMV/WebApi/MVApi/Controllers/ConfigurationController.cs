using Microsoft.AspNetCore.Mvc;
using QuickStars.MVApi.Business.Interfaces;

namespace QuickStars.MVApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService _configurationService;

        public ConfigurationController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        [HttpGet("GetAppSettings")]
        public async Task<IActionResult> GetAppSettings()
        {
            var appSettings = await _configurationService.GetAppSettings();
            return Ok(appSettings);
        }

        [HttpGet("GetApiSettings")]
        public async Task<IActionResult> GetApiSettings()
        {
            var apiSettings = await _configurationService.GetApiSettings();
            return Ok(apiSettings);
        }

        [HttpGet("GetDatabaseSettings")]
        public async Task<IActionResult> GetDatabaseSettings()
        {
            var databaseSettings = await _configurationService.GetDatabaseSettings();
            return Ok(databaseSettings);
        }

        [HttpGet("GetMessagingSettings")]
        public async Task<IActionResult> GetMessagingSettings()
        {
            var messagingSettings = await _configurationService.GetMessagingSettings();
            return Ok(messagingSettings);
        }

        [HttpGet("GetLoggingSettings")]
        public async Task<IActionResult> GetLoggingSettings()
        {
            var loggingSettings = await _configurationService.GetLoggingSettings();
            return Ok(loggingSettings);
        }

    }
}