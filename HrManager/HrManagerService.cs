using System.Text;
using Newtonsoft.Json;
using Nsu.Hackathon.Problem.Common.Dto;
using Nsu.Hackathon.Problem.Common.Mapper;
using Nsu.Hackathon.Problem.Common.Model;
using Nsu.Hackathon.Problem.HrManager.TeamBuilding;

namespace Nsu.Hackathon.Problem.HrManager;

public class HrManagerService(
    ITeamBuildingStrategy teamBuildingStrategy,
    HrManagerControllerOptions hrManagerServiceOptions,
    PreferenceMapper preferenceMapper,
    TeamMapper teamMapper)
{
    private readonly object _lock = new();
    
    private readonly List<Preference> _preferences = [];

    public async void AddPreference(PreferenceDto preferenceDto)
    {
        var preference = preferenceMapper.PreferenceDtoToPreference(preferenceDto);
        _preferences.Add(preference);
        List<Team> teams = null!;

        lock (_lock)
        {
            _preferences.Add(preference);
            if (_preferences.Count == hrManagerServiceOptions.ExpectedPreferencesAmount)
            {
                var teamLeadsPreferences = _preferences.Where(x => x.Employee is TeamLead)
                    .ToList();
                var juniorsPreferences = _preferences.Where(x => x.Employee is Junior)
                    .ToList();
                teams = BuildTeams(teamLeadsPreferences, juniorsPreferences);
                _preferences.Clear();
            }
        }

        if (teams != null)
        {
            await SendTeams(teams);
        }
    }

    public List<Team> BuildTeams(List<Preference> teamLeadsPreferences, List<Preference> juniorsPreferences)
    {
        return teamBuildingStrategy.BuildTeams(teamLeadsPreferences, juniorsPreferences);
    }

    private async Task SendTeams(List<Team> teams)
    {
        var preferencesDtos = preferenceMapper.PreferenceToPreferenceDto(_preferences);
        var teamsDtos = teamMapper.TeamToTeamDto(teams);

        var requestBody = new PreferencesAndTeamDto(
            preferencesDtos,
            teamsDtos
        );

        var requestBodyJson = JsonConvert.SerializeObject(requestBody);
        using var client = new HttpClient();
        await client.PostAsync("http://localhost:5002/api/hr-director/teams",
            new StringContent(requestBodyJson, Encoding.UTF8, "application/json"));
    }
}