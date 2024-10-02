namespace Nsu.Hackathon.Problem.Calculator;

public static class AverageCalculator
{
    public static double CalculateAverage(List<double> values)
    {
        var sum = values.Sum();

        return sum / values.Count;
    }
}