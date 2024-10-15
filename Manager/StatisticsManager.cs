namespace Nsu.Hackathon.Problem.Manager;

using Preferences;
using Calculator;
using TeamBuilding;

public class StatisticsManager
{
    private readonly List<int> _allHackathonsSatisfactionIndexes = [];
    private readonly List<double> _allHackathonsHarmonicMeans = [];
    public double CurrentHackathonHarmonicMean { get; private set; }
    public double TotalHarmonicMeanAverage { get; private set; }
    public double TotalHarmonicMean { get; private set; }

    public void SaveStatistics(List<Team> teams,
        List<Wishlist> teamLeadsWishlists, List<Wishlist> juniorsWishlists)
    {
        var indexes = SatisfactionCalculator.CalculateSatisfaction(teams, teamLeadsWishlists, juniorsWishlists);
        _allHackathonsSatisfactionIndexes.AddRange(indexes);

        CurrentHackathonHarmonicMean = HarmonicMeanCalculator.CalculateHarmonicMean(indexes);
        _allHackathonsHarmonicMeans.Add(CurrentHackathonHarmonicMean);
    }

    public void SummarizeResults()
    {
        TotalHarmonicMeanAverage = _allHackathonsHarmonicMeans.Average();
        TotalHarmonicMean = HarmonicMeanCalculator.CalculateHarmonicMean(_allHackathonsSatisfactionIndexes);
    }
}