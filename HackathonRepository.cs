using Microsoft.EntityFrameworkCore;
using Nsu.Hackathon.Problem.Hackathon;
using Nsu.Hackathon.Problem.Worker;

namespace Nsu.Hackathon.Problem;

public class HackathonRepository(HackathonContext hackathonContext)
{
    public HackathonEntity? GetHackathonById(long hackathonId)
    {
        return hackathonContext.Hackathons
            .Include(h => h.Participants)
            .ThenInclude(p => p.Participant)
            .Include(h => h.Wishlists)
            .Include(h => h.Teams)
            .AsSplitQuery()
            .FirstOrDefault(h => h.Id == hackathonId);
    }

    public List<HackathonEntity> GetAllHackathons()
    {
        return hackathonContext.Hackathons
            .ToList();
    }

    public List<Employee> GetJuniors()
    {
        return hackathonContext.Juniors.Cast<Employee>().ToList();
    }

    public List<Employee> GetTeamLeads()
    {
        return hackathonContext.TeamLeads.Cast<Employee>().ToList();
    }

    public long SaveHackathon(HackathonEntity hackathon)
    {
        hackathonContext.Hackathons.Add(hackathon);
        hackathonContext.SaveChanges();
        return hackathon.Id;
    }

    public void AddEmployeesIfItIsEmpty(EmployeeRepository employeeRepository)
    {
        if (hackathonContext.Employees.Any()) return;
        hackathonContext.Employees.AddRange(employeeRepository.Juniors);
        hackathonContext.Employees.AddRange(employeeRepository.TeamLeads);
        hackathonContext.SaveChanges();
    }

    public void EnsureCreated()
    {
        hackathonContext.Database.EnsureCreated();
    }
}