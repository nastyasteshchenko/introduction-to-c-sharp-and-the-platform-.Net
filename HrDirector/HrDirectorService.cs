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
    HackathonEventManager hackathonEventManager)
{
    private const string LineSeparator = "------------------------------------------";

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
        PrintHackathonInfo(id);

        if (hackathonEventManager.IsNeedNextHackathon())
        {
            hackathonEventManager.StartNewHackathon();
        }
        else
        {
            hackathonEventManager.StopHackathons();
        }

        return id;
    }

    private void PrintHackathonInfo(long hackathonId)
    {
        var hackathon = hackathonRepository.GetHackathonById(hackathonId);
        if (hackathon is null)
        {
            Console.WriteLine("No hackathon with id " + hackathonId);
            return;
        }

        Console.WriteLine($"Information about hackathon with id {hackathonId}");
        Console.WriteLine("Participants:");
        foreach (var participant in hackathon.Participants)
        {
            Console.WriteLine(participant.Participant);
        }

        Console.WriteLine(LineSeparator);
        Console.WriteLine("Teams:");
        foreach (var team in hackathon.Teams)
        {
            Console.WriteLine(team.TeamEntity);
        }

        Console.WriteLine(LineSeparator);
        Console.WriteLine($"Harmonic mean: {hackathon.HarmonicMean:0.000}");

        PrintAllHackathonsHarmonicMean();
    }

    public double CalculateStatistics
        (List<Team> teams, List<Preference> teamLeadsWishlists, List<Preference> juniorsWishlists)
    {
        var indexes = SatisfactionCalculator.CalculateSatisfaction(teams, teamLeadsWishlists, juniorsWishlists);
        return HarmonicMeanCalculator.CalculateHarmonicMean(indexes);
    }

    private void PrintAllHackathonsHarmonicMean()
    {
        var hackathons = hackathonRepository.GetAllHackathons();
        var harmonicMeans = hackathons.Select(h => h.HarmonicMean).ToList();
        var harmonicMeansAverage = harmonicMeans.Average();
        Console.WriteLine(LineSeparator);
        Console.WriteLine($"Total harmonic mean average: {harmonicMeansAverage:0.000}");
        Console.WriteLine(LineSeparator);
    }
}