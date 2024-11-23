using Common.Dto;
using Common.Mapper;
using Common.Model;
using HrDirector;
using HrDirector.DataBase;
using HrDirector.DataBase.Mapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Moq;

namespace Test;

public class HackathonBdTest
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
    public void HolingHackathonWithDataBaseTest()
    {
        var junior1 = CreateJunior(1, "Юдин Адам");
        var junior2 = CreateJunior(2, "Яшина Яна");
        var junior3 = CreateJunior(3, "Никитина Вероника");
        var junior4 = CreateJunior(4, "Рябинин Александр");

        var teamLead1 = CreateTeamLead(5, "Филиппова Ульяна");
        var teamLead2 = CreateTeamLead(6, "Николаев Григорий");
        var teamLead3 = CreateTeamLead(7, "Андреева Вероника");
        var teamLead4 = CreateTeamLead(8, "Коротков Михаил");

        var participants = new List<Employee>
            { junior1, junior2, junior3, junior4, teamLead1, teamLead2, teamLead3, teamLead4 };

        var teamLeadsWishlists = new List<Preference>
        {
            new(teamLead1, [junior2, junior1, junior3, junior4]),
            new(teamLead2, [junior1, junior4, junior3, junior2]),
            new(teamLead3, [junior3, junior2, junior1, junior4]),
            new(teamLead4, [junior3, junior4, junior2, junior4]),
        };

        var juniorsWishlists = new List<Preference>
        {
            new(junior1, [teamLead2, teamLead1, teamLead3, teamLead4]),
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

        var context = CreateInMemoryContext();
        var hackathonRepository = new HackathonRepository(context);

        var employeeMapper = new EmployeeMapper();
        var preferenceMapper = new PreferenceMapper(employeeMapper);
        var teamMapper = new TeamMapper(employeeMapper);
        var employeeEntityMapper = new EmployeeEntityMapper(context);
        var teamEntityMapper = new TeamEntityMapper(employeeEntityMapper);

        var applicationLifetime = new Mock<IHostApplicationLifetime>();
        var publishEndpoint = new Mock<IPublishEndpoint>();
        var hackathonEventManager = new HackathonEventManager(new Options(10), 
            publishEndpoint.Object, applicationLifetime.Object);
        var hrDirector = new HrDirectorService(preferenceMapper, teamMapper, employeeEntityMapper,
            teamEntityMapper, hackathonRepository, hackathonEventManager);

        var juniorPreferencesDto = preferenceMapper.PreferenceToPreferenceDto(juniorsWishlists)
            .ToList();
        var teamLeadPreferencesDto = preferenceMapper.PreferenceToPreferenceDto(teamLeadsWishlists)
            .ToList();
        var teamsDto = teamMapper.TeamToTeamDto(expectedTeams);

        var hackathonId = hrDirector.SummarizeAndSaveHackathon(new PreferencesAndTeamsDto(
            juniorPreferencesDto.Concat(teamLeadPreferencesDto).ToList(),
            teamsDto));

        var hackathonEntity = hackathonRepository.GetHackathonById(hackathonId);

        Assert.That(hackathonEntity, Is.Not.Null);
        Assert.That(hackathonEntity.Id, Is.EqualTo(hackathonId));
        Assert.That(Math.Round(hackathonEntity.HarmonicMean, 2), Is.EqualTo(2.67));

        Assert.That(hackathonEntity.Participants, Has.Count.EqualTo(participants.Count));
        var hackathonParticipants = hackathonEntity.Participants.Select(p => p.Participant)
            .ToList();
        foreach (var participant in participants)
        {
            Assert.That(hackathonParticipants,
                Does.Contain(employeeEntityMapper.EmployeeToEmployeeEntity(participant)));
        }

        Assert.That(hackathonEntity.Teams, Has.Count.EqualTo(expectedTeams.Count));
        var hackathonTeams = hackathonEntity.Teams.Select(t => t.TeamEntity).ToList();
        foreach (var team in expectedTeams)
        {
            Assert.That(hackathonTeams, Does.Contain(teamEntityMapper.TeamToTeamEntity(team)));
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