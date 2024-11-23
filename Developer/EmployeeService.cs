using System.Text;
using Common.Mapper;
using Common.Message;
using Common.Model;
using MassTransit;
using Newtonsoft.Json;

namespace Developer;

public class EmployeeService(
    EmployeeRepository employeeRepository,
    EmployeeWorkerOptions options,
    PreferenceMapper preferenceMapper,
    IBus bus
)
{
    public async void HandleStartHackathonMessage(StartHackathonMessage message)
    {
        Console.WriteLine($"Hackathon №{message.HackathonId} was started.");
        var preference = GetPreference(options.Type, options.Id);
        var preferenceDto = preferenceMapper.PreferenceToPreferenceDto(preference);
        var prefMessage = new PreferencesMessage
        {
            Preference = preferenceDto
        };
        await bus.Publish(prefMessage);
    }

    private Preference GetPreference(string employeeType, long employeeId)
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