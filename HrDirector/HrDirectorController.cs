using Microsoft.AspNetCore.Mvc;
using Nsu.Hackathon.Problem.Common.Dto;

namespace Nsu.Hackathon.Problem.HrDirector;

[ApiController]
[Route("api/hr-director")]
public class HrDirectorController(HrDirectorService hrDirectorService) : ControllerBase
{
    [HttpPost("teams")]
    public IActionResult ReceivePreferences([FromBody] PreferencesAndTeamDto preferencesAndTeamDto)
    {
        hrDirectorService.SummarizeAndSaveHackathon(preferencesAndTeamDto);
        return Ok();
    }
}