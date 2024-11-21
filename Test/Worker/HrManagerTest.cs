using Moq;
using Nsu.Hackathon.Problem.Common.Mapper;
using Nsu.Hackathon.Problem.Common.Model;
using Nsu.Hackathon.Problem.HrManager;
using Nsu.Hackathon.Problem.HrManager.TeamBuilding;

namespace Test.Worker;

public class HrManagerTest
{
    [Test]
    public void TeamBuildingStrategyCallingTimes()
    {
        var teamLeadsWishlists = new List<Preference>();
        var juniorsWishlists = new List<Preference>();

        var mockStrategy = new Mock<ITeamBuildingStrategy>();

        mockStrategy
            .Setup(strategy => strategy.BuildTeams(teamLeadsWishlists, juniorsWishlists))
            .Returns([]);

        var employeeMapper = new EmployeeMapper();
        var preferenceMapper = new PreferenceMapper(employeeMapper);
        var teamMapper = new TeamMapper(employeeMapper);

        var hrManager = new HrManagerService(mockStrategy.Object,
            new HrManagerControllerOptions(8),
            preferenceMapper, teamMapper);

        hrManager.BuildTeams(teamLeadsWishlists, juniorsWishlists);

        mockStrategy.Verify(strategy => strategy.BuildTeams(teamLeadsWishlists, juniorsWishlists), Times.Once);
    }

    [Test]
    public void TeamBuildingResultTest()
    {
        var junior1 = CreateJunior(1, "Юдин Адам");
        var junior2 = CreateJunior(2, "Яшина Яна");
        var junior3 = CreateJunior(3, "Никитина Вероника");
        var junior4 = CreateJunior(4, "Рябинин Александр");

        var teamLead1 = CreateTeamLead(5, "Филиппова Ульяна");
        var teamLead2 = CreateTeamLead(6, "Николаев Григорий");
        var teamLead3 = CreateTeamLead(7, "Андреева Вероника");
        var teamLead4 = CreateTeamLead(8, "Коротков Михаил");

        var teamLeadsWishlists = new List<Preference>
        {
            new(teamLead1, [junior2, junior1, junior3, junior4]),
            new(teamLead2, [junior1, junior4, junior3, junior2]),
            new(teamLead3, [junior3, junior2, junior1, junior4]),
            new(teamLead4, [junior3, junior4, junior2, junior4]),
        };

        var juniorsWishlists = new List<Preference>
        {
            new(junior1, [teamLead1, teamLead2, teamLead3, teamLead4]),
            new(junior2, [teamLead1, teamLead3, teamLead4, teamLead2]),
            new(junior3, [teamLead4, teamLead3, teamLead1, teamLead2]),
            new(junior4, [teamLead2, teamLead4, teamLead3, teamLead1]),
        };

        var expectedTeams = new List<Team>
        {
            CreateTeam(teamLead1, junior2),
            CreateTeam(teamLead2, junior1),
            CreateTeam(teamLead3, junior4),
            CreateTeam(teamLead4, junior3)
        };

        var employeeMapper = new EmployeeMapper();
        var preferenceMapper = new PreferenceMapper(employeeMapper);
        var teamMapper = new TeamMapper(employeeMapper);

        var hrManager = new HrManagerService(new TeamBuildingStrategy(),
            new HrManagerControllerOptions(8),
            preferenceMapper, teamMapper);

        var actualTeams = hrManager.BuildTeams(teamLeadsWishlists, juniorsWishlists);

        Assert.That(actualTeams, Has.Count.EqualTo(expectedTeams.Count));
        foreach (var team in expectedTeams)
        {
            Assert.That(actualTeams, Has.Member(team));
        }
    }

    private static Team CreateTeam(TeamLead teamLead, Junior junior)
    {
        return new Team(junior, teamLead);
    }

    private static Junior CreateJunior(long id, string name)
    {
        return new Junior(id, name);
    }

    private static TeamLead CreateTeamLead(long id, string name)
    {
        return new TeamLead(id, name);
    }
}