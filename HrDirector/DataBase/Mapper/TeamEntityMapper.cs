using Nsu.Hackathon.Problem.Common.Model;
using Nsu.Hackathon.Problem.HrDirector.DataBase.Model;

namespace Nsu.Hackathon.Problem.HrDirector.DataBase.Mapper;

public class TeamEntityMapper(EmployeeEntityMapper employeeEntityMapper)
{
    public List<TeamEntity> TeamToTeamEntity(List<Team> teams)
    {
        return teams.Select(TeamToTeamEntity).ToList();
    }

    public TeamEntity TeamToTeamEntity(Team team)
    {
        return new TeamEntity
        {
            Junior = employeeEntityMapper.EmployeeToEmployeeEntity(team.Junior),
            TeamLead = employeeEntityMapper.EmployeeToEmployeeEntity(team.TeamLead),
            JuniorId = team.Junior.Id,
            TeamLeadId = team.TeamLead.Id
        };
    }
}