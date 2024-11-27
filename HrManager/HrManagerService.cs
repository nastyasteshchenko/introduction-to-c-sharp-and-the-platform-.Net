using System.Text;
using Common.Mapper;
using Common.Message;
using Common.Model;
using HrManager.TeamBuilding;
using Newtonsoft.Json;

namespace HrManager;

public class HrManagerService(
    ITeamBuildingStrategy teamBuildingStrategy,
    Options serviceOptions,
    PreferenceMapper preferenceMapper,
    TeamMapper teamMapper)
{
    private readonly object _lock = new();

    private readonly List<Preference> _preferences = [];

    public async void AddPreference(SendPreferences sendPreferences)
    {
        var preferenceDto = sendPreferences.Preference;
        var preference = preferenceMapper.PreferenceDtoToPreference(preferenceDto);
        List<Team>? teams = null;
        List<Preference>? preferences = null;

        lock (_lock)
        {
            _preferences.Add(preference);
            if (_preferences.Count == serviceOptions.ExpectedPreferencesAmount)
            {
                var teamLeadsPreferences = _preferences.Where(x => x.Employee is TeamLead)
                    .ToList();
                var juniorsPreferences = _preferences.Where(x => x.Employee is Junior)
                    .ToList();
                teams = BuildTeams(teamLeadsPreferences, juniorsPreferences);
                preferences = [.._preferences];
                _preferences.Clear();
            }
        }

        if (teams != null && preferences != null)
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
        var teamsDtos = teamMapper.TeamToTeamDto(teams);

        var requestBodyJson = JsonConvert.SerializeObject(teamsDtos);
        using var client = new HttpClient();
        await client.PostAsync("http://hr-director:8080/api/hr-director/teams",
            new StringContent(requestBodyJson, Encoding.UTF8, "application/json"));
    }
}