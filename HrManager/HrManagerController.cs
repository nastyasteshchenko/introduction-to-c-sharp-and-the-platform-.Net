using Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HrManager;

[ApiController]
[Route("api/hr-manager")]
public class HrManagerController(HrManagerService hrManagerService) : ControllerBase
{
    
    [HttpPost("preferences")]
    public IActionResult ReceivePreferences([FromBody] PreferenceDto preference)
    {
        hrManagerService.AddPreference(preference);
        return Ok();
    }
}