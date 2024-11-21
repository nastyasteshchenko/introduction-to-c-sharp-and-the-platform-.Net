using Common.Dto;
using Common.Model;

namespace Common.Mapper;

public class TeamMapper(EmployeeMapper employeeMapper)
{
    public List<TeamDto> TeamToTeamDto(List<Team> teams)
    {
        return teams.Select(TeamToTeamDto).ToList();
    }

    public TeamDto TeamToTeamDto(Team team)
    {
        var junior = employeeMapper.EmployeeToEmployeeDto(team.Junior);
        var teamLead = employeeMapper.EmployeeToEmployeeDto(team.TeamLead);
        return new TeamDto(junior, teamLead);
    }

    public List<Team> TeamDtoToTeam(List<TeamDto> teamsDtos)
    {
        return teamsDtos.Select(TeamDtoToTeam).ToList();
    }

    public Team TeamDtoToTeam(TeamDto teamDto)
    {
        var junior = employeeMapper.EmployeeDtoToEmployee(teamDto.Junior);
        var teamLead = employeeMapper.EmployeeDtoToEmployee(teamDto.TeamLead);
        return new Team(junior, teamLead);
    }
}