using Microsoft.EntityFrameworkCore;
using Nsu.Hackathon.Problem.Common.Mapper;
using Nsu.Hackathon.Problem.Common.Model;
using Nsu.Hackathon.Problem.HrDirector;
using Nsu.Hackathon.Problem.HrDirector.DataBase;
using Nsu.Hackathon.Problem.HrDirector.DataBase.Mapper;

namespace Test.Worker;

public class HrDirectorTest
{
    private static HackathonContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<HackathonContext>()
            .UseSqlite("Data Source=:memory:").Options;
        var context = new HackathonContext(options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();
        return context;
    }

    [Test]
    public void HarmonicMeanForTeamTest()
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

        var teams = new List<Team>
        {
            CreateTeam(teamLead1, junior2),
            CreateTeam(teamLead2, junior1),
            CreateTeam(teamLead3, junior4),
            CreateTeam(teamLead4, junior3)
        };

        var context = CreateInMemoryContext();
        var hackathonRepository = new HackathonRepository(context);
        
        var employeeMapper = new EmployeeMapper();
        var preferenceMapper = new PreferenceMapper(employeeMapper);
        var teamMapper = new TeamMapper(employeeMapper);
        var employeeEntityMapper = new EmployeeEntityMapper(context);
        var teamEntityMapper = new TeamEntityMapper(employeeEntityMapper);

        var director = new HrDirectorService(preferenceMapper, teamMapper, employeeEntityMapper,
            teamEntityMapper, hackathonRepository);
        var currentHackathonHarmonicMean =
            director.CalculateStatistics(teams, teamLeadsWishlists, juniorsWishlists);

        Assert.That(Math.Round(currentHackathonHarmonicMean, 2),
            Is.EqualTo(2.59));

        teams =
        [
            CreateTeam(teamLead1, junior1),
            CreateTeam(teamLead2, junior2),
            CreateTeam(teamLead3, junior3),
            CreateTeam(teamLead4, junior4)
        ];

        currentHackathonHarmonicMean = director.CalculateStatistics(teams, teamLeadsWishlists, juniorsWishlists);

        Assert.That(Math.Round(currentHackathonHarmonicMean, 2),
            Is.EqualTo(2.09));
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