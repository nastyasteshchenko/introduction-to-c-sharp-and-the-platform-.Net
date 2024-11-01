using Nsu.Hackathon.Problem.Calculator;

namespace Test;

public class HarmonicMeanCalculatorTest
{
    [Test]
    public void SingleValueTest()
    {
        var values = new List<int> { 2 };

        Assert.That(HarmonicMeanCalculator.CalculateHarmonicMean(values), Is.EqualTo(2));
    }

    [Test]
    public void SameValuesTest()
    {
        var values = new List<int> { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };

        Assert.That(HarmonicMeanCalculator.CalculateHarmonicMean(values), Is.EqualTo(10));
    }

    [Test]
    public void DifferentValuesTest()
    {
        var values = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        Assert.That(Math.Round(HarmonicMeanCalculator.CalculateHarmonicMean(values), 2),
            Is.EqualTo(3.18));

        values = [6, 2];

        Assert.That(HarmonicMeanCalculator.CalculateHarmonicMean(values), Is.EqualTo(3));
    }
}