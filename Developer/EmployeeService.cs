using Common.Mapper;
using Common.Message;
using Common.Model;
using MassTransit;

namespace Developer;

public class EmployeeService(
    EmployeeRepository employeeRepository,
    Options options,
    PreferenceMapper preferenceMapper,
    IPublishEndpoint publishEndpoint
)
{
    public async void HandleStartHackathonMessage(HackathonStarted started)
    {
        Console.WriteLine($"Hackathon №{started.HackathonId} was started.");
        var preference = GetPreference(options.Type, options.Id);
        var preferenceDto = preferenceMapper.PreferenceToPreferenceDto(preference);
        var prefMessage = new SendPreferences
        {
            Preference = preferenceDto
        };
        await publishEndpoint.Publish(prefMessage);
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