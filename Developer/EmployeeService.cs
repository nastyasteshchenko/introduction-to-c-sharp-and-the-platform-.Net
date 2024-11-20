using Nsu.Hackathon.Problem.Common.Model;

namespace Nsu.Hackathon.Problem.Developer;

public class EmployeeService(EmployeeRepository employeeRepository)
{
    public Preference GetPreference(string employeeType, long employeeId)
    {
        if (employeeType == "junior")
        {
            var junior = employeeRepository.Juniors.Find(employee => employee.Id == employeeId)!;
            return PreferencesGenerator.GeneratePreference(junior, employeeRepository.TeamLeads);
        }

        var teamLead = employeeRepository.TeamLeads.Find(employee => employee.Id == employeeId)!;
        return PreferencesGenerator.GeneratePreference(teamLead, employeeRepository.Juniors);
    }
}