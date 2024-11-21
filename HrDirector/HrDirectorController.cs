using Common.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HrDirector;

[ApiController]
[Route("api/hr-director")]
public class HrDirectorController(HrDirectorService hrDirectorService) : ControllerBase
{
    [HttpPost("teams")]
    public IActionResult ReceivePreferencesAndTeams([FromBody] PreferencesAndTeamsDto preferencesAndTeamsDto)
    {
        hrDirectorService.SummarizeAndSaveHackathon(preferencesAndTeamsDto);
        return Ok();
    }
}