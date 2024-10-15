using Nsu.Hackathon.Problem.Manager;
using Nsu.Hackathon.Problem.Preferences;
using Nsu.Hackathon.Problem.TeamBuilding;

namespace Nsu.Hackathon.Problem.Worker;

public class HrDirector
{
    private readonly StatisticsManager _statisticsManager = new();

    public void CalculateStatistics(List<Team> teams,
        List<Wishlist> teamLeadsWishlists, List<Wishlist> juniorsWishlists)
    {
        _statisticsManager.CalculateStatistics(teams, teamLeadsWishlists, juniorsWishlists);
    }

    public void SummarizeResults()
    {
        _statisticsManager.SummarizeResults();
    }

    public void SayCurrentHackathonStatistics()
    {
        Console.WriteLine($"Current harmonic mean: {_statisticsManager.CurrentHackathonHarmonicMean:0.000}");
    }

    public void SayTotalHackathonsStatistics()
    {
        Console.WriteLine($"Total harmonic mean: {_statisticsManager.TotalHarmonicMean:0.000}");
        Console.WriteLine($"Total harmonic mean average: {_statisticsManager.TotalHarmonicMeanAverage:0.000}");
    }
}