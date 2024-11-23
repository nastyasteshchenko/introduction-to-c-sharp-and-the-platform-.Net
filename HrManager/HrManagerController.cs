using Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HrManager;

[ApiController]
[Route("api/hr-manager")]
public class HrManagerController(HrManagerService hrManagerService, IHostApplicationLifetime appLifetime)
    : ControllerBase
{
    [HttpPost("preferences")]
    public IActionResult ReceivePreferences([FromBody] PreferenceDto preference)
    {
        hrManagerService.AddPreference(preference);
        Task.Run(() =>
        {
            Task.Delay(4000).Wait();
            appLifetime.StopApplication();
        });
        return Ok();
    }
}