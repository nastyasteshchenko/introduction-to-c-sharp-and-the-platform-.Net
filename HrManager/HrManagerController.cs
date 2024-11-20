using Microsoft.AspNetCore.Mvc;
using Nsu.Hackathon.Problem.Common.Dto;

namespace Nsu.Hackathon.Problem.HrManager;

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