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
    HackathonInfoPrinter hackathonInfoPrinter,
    IHostApplicationLifetime appLifetime)
{
    public long SummarizeAndSaveHackathon(PreferencesAndTeamsDto preferencesAndTeamsDto)
    {
        hackathonRepository.EnsureCreated();
        var preferencesDtos = preferencesAndTeamsDto.Preferences;
        var teamDtos = preferencesAndTeamsDto.Teams;

        var preferences = preferenceMapper.PreferenceDtoToPreference(preferencesDtos);
        var teams = teamMapper.TeamDtoToTeam(teamDtos);
        var teamsEntities = teamEntityMapper.TeamToTeamEntity(teamMapper.TeamDtoToTeam(teamDtos));

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

        hackathon.HarmonicMean = CalculateStatistics(teams, teamLeadsPreferences, juniorsPreferences);

        var id = hackathonRepository.SaveHackathon(hackathon);
        hackathonInfoPrinter.PrintHackathonInfo(id);

        appLifetime.StopApplication();
        return id;
    }

    public double CalculateStatistics
        (List<Team> teams, List<Preference> teamLeadsWishlists, List<Preference> juniorsWishlists)
    {
        var indexes = SatisfactionCalculator.CalculateSatisfaction(teams, teamLeadsWishlists, juniorsWishlists);
        return HarmonicMeanCalculator.CalculateHarmonicMean(indexes);
    }
}