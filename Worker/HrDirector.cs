using Nsu.Hackathon.Problem.Calculator;
using Nsu.Hackathon.Problem.Preferences;
using Nsu.Hackathon.Problem.TeamBuilding;

namespace Nsu.Hackathon.Problem.Worker;

public class HrDirector
{
    public double CurrentHackathonHarmonicMean { get; private set; }
    private double _totalHarmonicMeanAverage;

    public void SaveStatistics
        (List<Team> teams, List<Preference> teamLeadsWishlists, List<Preference> juniorsWishlists)
    {
        var indexes = SatisfactionCalculator.CalculateSatisfaction(teams, teamLeadsWishlists, juniorsWishlists);
        CurrentHackathonHarmonicMean = HarmonicMeanCalculator.CalculateHarmonicMean(indexes);
    }

    public double CountTotalHarmonicMeanAverage(List<double> hackathonsHarmonicMean)
    {
        _totalHarmonicMeanAverage = hackathonsHarmonicMean.Average();
        return _totalHarmonicMeanAverage;
    }
    
}