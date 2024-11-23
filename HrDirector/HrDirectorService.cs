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
    HackathonInfoPrinter hackathonInfoPrinter)
{
    public long SummarizeAndSaveHackathon(PreferencesAndTeamsDto preferencesAndTeamsDto)
    {
        hackathonRepository.EnsureCreated();

        var preferences = GetPreferences(preferencesAndTeamsDto);
        var teamsDtos = teamMapper.TeamDtoToTeam(preferencesAndTeamsDto.Teams);
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

        return id;
    }

    public double CalculateStatistics
        (List<Team> teams, List<Preference> teamLeadsWishlists, List<Preference> juniorsWishlists)
    {
        var indexes = SatisfactionCalculator.CalculateSatisfaction(teams, teamLeadsWishlists, juniorsWishlists);
        return HarmonicMeanCalculator.CalculateHarmonicMean(indexes);
    }

    private List<Preference> GetPreferences(PreferencesAndTeamsDto preferencesAndTeamsDto)
    {
        var preferencesDtos = preferencesAndTeamsDto.Preferences;
        return preferenceMapper.PreferenceDtoToPreference(preferencesDtos);
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