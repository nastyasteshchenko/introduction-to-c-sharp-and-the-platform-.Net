using Nsu.Hackathon.Problem.Preferences;
using Nsu.Hackathon.Problem.TeamBuilding;
using Nsu.Hackathon.Problem.Worker;

namespace Test.Worker;

public class HrDirectorTest
{
    [Test]
    public void HarmonicMeanForTeamTest()
    {
        var junior1 = new Employee(1, "Юдин Адам");
        var junior2 = new Employee(2, "Яшина Яна");
        var junior3 = new Employee(3, "Никитина Вероника");
        var junior4 = new Employee(4, "Рябинин Александр");

        var teamLead1 = new Employee(1, "Филиппова Ульяна");
        var teamLead2 = new Employee(2, "Николаев Григорий");
        var teamLead3 = new Employee(3, "Андреева Вероника");
        var teamLead4 = new Employee(4, "Коротков Михаил");

        var teamLeadsWishlists = new List<Wishlist>
        {
            new(teamLead1, [junior2, junior1, junior3, junior4]),
            new(teamLead2, [junior1, junior4, junior3, junior2]),
            new(teamLead3, [junior3, junior2, junior1, junior4]),
            new(teamLead4, [junior3, junior4, junior2, junior4]),
        };

        var juniorsWishlists = new List<Wishlist>
        {
            new(junior1, [teamLead1, teamLead2, teamLead3, teamLead4]),
            new(junior2, [teamLead1, teamLead3, teamLead4, teamLead2]),
            new(junior3, [teamLead4, teamLead3, teamLead1, teamLead2]),
            new(junior4, [teamLead2, teamLead4, teamLead3, teamLead1]),
        };

        var teams = new List<Team>
        {
            new(teamLead1, junior2),
            new(teamLead2, junior1),
            new(teamLead3, junior4),
            new(teamLead4, junior3)
        };

        var director = new HrDirector();
        director.SaveStatistics(teams, teamLeadsWishlists, juniorsWishlists);

        Assert.That(Math.Round(director.CurrentHackathonHarmonicMean, 2),
            Is.EqualTo(2.59));
        
        teams =
        [
            new Team(teamLead1, junior1),
            new Team(teamLead2, junior2),
            new Team(teamLead3, junior3),
            new Team(teamLead4, junior4)
        ];
        
        director.SaveStatistics(teams, teamLeadsWishlists, juniorsWishlists);
        
        Assert.That(Math.Round(director.CurrentHackathonHarmonicMean, 2),
            Is.EqualTo(2.09));
    }
}