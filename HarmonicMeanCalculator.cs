namespace Nsu.Hackathon.Problem;

public static class HarmonicMeanCalculator
{
    public static double CalculateHarmonicMean(List<int> values)
    {
        var sumOfReciprocals = values.Sum(value => 1.0 / value);

        return values.Count / sumOfReciprocals;
    }
}