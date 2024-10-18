using Nsu.Hackathon.Problem.Calculator;
using Nsu.Hackathon.Problem.Preferences;
using Nsu.Hackathon.Problem.TeamBuilding;

namespace Nsu.Hackathon.Problem.Worker;

public class HrDirector
{
    public double CurrentHackathonHarmonicMean { get; private set; }

    private readonly List<int> _allHackathonsSatisfactionIndexes = [];
    private readonly List<double> _allHackathonsHarmonicMeans = [];
    private double _totalHarmonicMeanAverage;
    private double _totalHarmonicMean;

    public void SaveStatistics
        (List<Team> teams, List<Wishlist> teamLeadsWishlists, List<Wishlist> juniorsWishlists)
    {
        var indexes = SatisfactionCalculator.CalculateSatisfaction(teams, teamLeadsWishlists, juniorsWishlists);
        _allHackathonsSatisfactionIndexes.AddRange(indexes);

        CurrentHackathonHarmonicMean = HarmonicMeanCalculator.CalculateHarmonicMean(indexes);
        _allHackathonsHarmonicMeans.Add(CurrentHackathonHarmonicMean);
    }

    public void SummarizeResults()
    {
        _totalHarmonicMeanAverage = _allHackathonsHarmonicMeans.Average();
        _totalHarmonicMean = HarmonicMeanCalculator.CalculateHarmonicMean(_allHackathonsSatisfactionIndexes);
    }

    public void SayCurrentHackathonStatistics()
    {
        Console.WriteLine($"Current harmonic mean: {CurrentHackathonHarmonicMean:0.000}");
    }

    public void SayTotalHackathonsStatistics()
    {
        Console.WriteLine($"Total harmonic mean: {_totalHarmonicMean:0.000}");
        Console.WriteLine($"Total harmonic mean average: {_totalHarmonicMeanAverage:0.000}");
    }
}