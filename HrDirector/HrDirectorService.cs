using Nsu.Hackathon.Problem.Common.Dto;
using Nsu.Hackathon.Problem.Common.Mapper;
using Nsu.Hackathon.Problem.Common.Model;
using Nsu.Hackathon.Problem.HrDirector.Calculator;
using Nsu.Hackathon.Problem.HrDirector.DataBase;
using Nsu.Hackathon.Problem.HrDirector.DataBase.Model;
using Nsu.Hackathon.Problem.Web.HrDirector.Calculator;

namespace Nsu.Hackathon.Problem.HrDirector;

public class HrDirectorService(
    PreferenceMapper preferenceMapper,
    TeamMapper teamMapper,
    HackathonRepository hackathonRepository)
{
    private const string LineSeparator = "------------------------------------------";

    public long SummarizeAndSaveHackathon(PreferencesAndTeamDto preferencesAndTeamDto)
    {
        var preferencesDtos = preferencesAndTeamDto.Preferences;
        var teamDtos = preferencesAndTeamDto.Teams;

        var preferences = preferenceMapper.PreferenceDtoToPreference(preferencesDtos);
        var teams = teamMapper.TeamDtoToTeam(teamDtos);

        var hackathon = new HackathonEntity();

        var participants = preferences.Select(preference => preference.Employee).ToList();

        hackathon.AddParticipants(participants);

        hackathon.AddWishlists(preferences);

        hackathon.AddTeams(teams);

        var teamLeadsPreferences = preferences.Where(preference => preference.Employee is TeamLead)
            .ToList();
        foreach (var f in teamLeadsPreferences)
        {
            Console.WriteLine(f.Employee);
        }
        var juniorsPreferences = preferences.Where(preference => preference.Employee is Junior)
            .ToList();
        foreach (var f in juniorsPreferences)
        {
            Console.WriteLine(f.Employee);
        }
        hackathon.HarmonicMean = CalculateStatistics(teams, teamLeadsPreferences, juniorsPreferences);

        var id = hackathonRepository.SaveHackathon(hackathon);
        PrintHackathonInfo(id);
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
            Console.WriteLine(team.Team);
        }

        Console.WriteLine(LineSeparator);

        Console.WriteLine($"Harmonic mean: {hackathon.HarmonicMean:0.000}");
    }

    public double CalculateStatistics
        (List<Team> teams, List<Preference> teamLeadsWishlists, List<Preference> juniorsWishlists)
    {
        var indexes = SatisfactionCalculator.CalculateSatisfaction(teams, teamLeadsWishlists, juniorsWishlists);
        return HarmonicMeanCalculator.CalculateHarmonicMean(indexes);
    }

    public double CountTotalHarmonicMeanAverage(List<double> hackathonsHarmonicMean)
    {
        return hackathonsHarmonicMean.Average();
    }
}