using System.Text;
using System.Text.Json.Serialization;
using Common.Dto;
using Common.Mapper;
using Common.Model;
using HrManager.TeamBuilding;
using Newtonsoft.Json;

namespace HrManager;

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
        await client.PostAsync("http://hr-director:8080/api/hr-director/teams",
            new StringContent(requestBodyJson, Encoding.UTF8, "application/json"));
    }
}