using Common.Dto;
using Common.Mapper;
using Common.Model;
using HrDirector.Calculator;
using HrDirector.DataBase;
using HrDirector.DataBase.Mapper;
using HrDirector.DataBase.Model.Hackathon;

namespace HrDirector;

public class HrDirectorService(
    PreferenceMapper preferenceMapper,
    TeamMapper teamMapper,
    EmployeeEntityMapper employeeEntityMapper,
    TeamEntityMapper teamEntityMapper,
    HackathonRepository hackathonRepository,
    HackathonEventManager hackathonEventManager,
    HackathonInfoPrinter hackathonInfoPrinter,
    Options options)
{
    private readonly object _lock = new();
    private readonly List<TeamDto> _teamsDtos = [];
    private readonly List<PreferenceDto> _preferencesDtos = [];

    public void HandleTeamsMessage(List<TeamDto> teams)
    {
        lock (_lock)
        {
            _teamsDtos.AddRange(teams);
            if (_preferencesDtos.Count == options.PreferencesAmount)
            {
                SummarizeAndSaveHackathon();
            }
        }
    }

    public void HandleSendPreferencesMessage(PreferenceDto preference)
    {
        lock (_lock)
        {
            _preferencesDtos.Add(preference);
            if (_preferencesDtos.Count == options.PreferencesAmount && _teamsDtos.Count > 0)
            {
                SummarizeAndSaveHackathon();
            }
        }
    }

    private void SummarizeAndSaveHackathon()
    {
        hackathonRepository.EnsureCreated();

        var preferences = preferenceMapper.PreferenceDtoToPreference(_preferencesDtos);
        var teamsDtos = teamMapper.TeamDtoToTeam(_teamsDtos);

        _teamsDtos.Clear();
        _preferencesDtos.Clear();

        var teamsEntities = teamEntityMapper.TeamToTeamEntity(teamsDtos);

        var hackathon = new HackathonEntity();

        var participants =
            employeeEntityMapper.EmployeeToEmployeeEntity(preferences.Select(preference => preference.Employee)
                .ToList());

        hackathon.AddParticipants(participants);
        hackathon.AddWishlists(preferences, employeeEntityMapper);
        hackathon.AddTeams(teamsEntities);

        var teamLeadsPreferences = preferences.Where(preference => preference.Employee is TeamLead)
            .ToList();
        var juniorsPreferences = preferences.Where(preference => preference.Employee is Junior)
            .ToList();

        hackathon.HarmonicMean = CalculateStatistics(teamsDtos, teamLeadsPreferences, juniorsPreferences);

        var id = hackathonRepository.SaveHackathon(hackathon);
        hackathonInfoPrinter.PrintHackathonInfo(id);

        DecideIfNeedNewHackathon();
    }

    private double CalculateStatistics
        (List<Team> teams, List<Preference> teamLeadsWishlists, List<Preference> juniorsWishlists)
    {
        var indexes = SatisfactionCalculator.CalculateSatisfaction(teams, teamLeadsWishlists, juniorsWishlists);
        return HarmonicMeanCalculator.CalculateHarmonicMean(indexes);
    }

    private void DecideIfNeedNewHackathon()
    {
        if (hackathonEventManager.IsNeedNextHackathon())
        {
            hackathonEventManager.StartNewHackathon();
        }
        else
        {
            hackathonEventManager.StopHackathons();
        }
    }
}