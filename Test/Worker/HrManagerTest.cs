using Moq;
using Nsu.Hackathon.Problem.Preferences;
using Nsu.Hackathon.Problem.TeamBuilding;
using Nsu.Hackathon.Problem.Worker;

namespace Test.Worker;

public class HrManagerTest
{
    [Test]
    public void TeamBuildingStrategyCallingTimes()
    {
        var teamLeadsWishlists = new List<Wishlist>();
        var juniorsWishlists = new List<Wishlist>();

        var mockStrategy = new Mock<ITeamBuildingStrategy>();

        mockStrategy
            .Setup(strategy => strategy.BuildTeams(teamLeadsWishlists, juniorsWishlists))
            .Returns([]);

        var hrManager = new HrManager(mockStrategy.Object);

        hrManager.BuildTeams(teamLeadsWishlists, juniorsWishlists);

        mockStrategy.Verify(strategy => strategy.BuildTeams(teamLeadsWishlists, juniorsWishlists), Times.Once);
    }

    [Test]
    public void TeamBuildingResultTest()
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

        var expectedTeams = new List<Team>
        {
            new(teamLead1, junior2),
            new(teamLead2, junior1),
            new(teamLead3, junior4),
            new(teamLead4, junior3)
        };

        var hrManager = new HrManager(new TeamBuildingStrategy());

        var actualTeams = hrManager.BuildTeams(teamLeadsWishlists, juniorsWishlists);

        Assert.That(actualTeams, Has.Count.EqualTo(expectedTeams.Count));
        foreach (var team in expectedTeams)
        {
            Assert.That(actualTeams, Has.Member(team));
        }
    }
}