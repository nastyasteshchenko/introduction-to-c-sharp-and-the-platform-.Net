using Nsu.Hackathon.Problem.Common.Dto;
using Nsu.Hackathon.Problem.Common.Model;

namespace Nsu.Hackathon.Problem.Common.Mapper;

public class PreferenceMapper(EmployeeMapper employeeMapper)
{
    public List<PreferenceDto> PreferenceToPreferenceDto(List<Preference> preferences)
    {
        return preferences.Select(PreferenceToPreferenceDto).ToList();
    }

    public PreferenceDto PreferenceToPreferenceDto(Preference preference)
    {
        var desiredEmployeesDto = employeeMapper.EmployeeToEmployeeDto(preference.DesiredEmployees);
        return new PreferenceDto(employeeMapper.EmployeeToEmployeeDto(preference.Employee), desiredEmployeesDto);
    }

    public List<Preference> PreferenceDtoToPreference(List<PreferenceDto> preferencesDtos)
    {
        return preferencesDtos.Select(PreferenceDtoToPreference).ToList();
    }

    public Preference PreferenceDtoToPreference(PreferenceDto preferenceDto)
    {
        var desiredEmployees = employeeMapper.EmployeeDtoToEmployee(preferenceDto.DesiredEmployees);
        return new Preference(employeeMapper.EmployeeDtoToEmployee(preferenceDto.Employee), desiredEmployees);
    }
}