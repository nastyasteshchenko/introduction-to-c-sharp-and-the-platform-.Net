namespace Nsu.Hackathon.Problem.Manager;

using Preferences;
using Calculator;
using TeamBuilding;

public class StatisticsManager
{
    private readonly List<int> _allHackathonsSatisfactionIndexes = [];
    private readonly List<double> _allHackathonsHarmonicMeans = [];
    private double _currentHackathonHarmonicMean;
    private double _totalHarmonicMeanAverage;
    private double _totalHarmonicMean;

    public void AddStatistics(List<Team> teams,
        List<Wishlist> teamLeadsWishlists, List<Wishlist> juniorsWishlists)
    {
        var indexes = SatisfactionCalculator.CalculateSatisfaction(teams, teamLeadsWishlists, juniorsWishlists);
        _allHackathonsSatisfactionIndexes.AddRange(indexes);

        _currentHackathonHarmonicMean = HarmonicMeanCalculator.CalculateHarmonicMean(indexes);
        _allHackathonsHarmonicMeans.Add(_currentHackathonHarmonicMean);
    }

    public void SummarizeResults()
    {
        _totalHarmonicMeanAverage = AverageCalculator.CalculateAverage(_allHackathonsHarmonicMeans);
        _totalHarmonicMean = HarmonicMeanCalculator.CalculateHarmonicMean(_allHackathonsSatisfactionIndexes);
    }

    public void PrintCurrentHarmonicMean()
    {
        Console.WriteLine($"Current harmonic mean: {_currentHackathonHarmonicMean:0.000}");
    }

    public void PrintResults()
    {
        Console.WriteLine($"Total harmonic mean: {_totalHarmonicMean:0.000}");
        Console.WriteLine($"Total harmonic mean average: {_totalHarmonicMeanAverage:0.000}");
    }
}