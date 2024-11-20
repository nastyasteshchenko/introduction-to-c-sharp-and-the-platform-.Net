namespace Nsu.Hackathon.Problem.Common.Dto;

public record PreferenceDto(EmployeeDto Employee, List<EmployeeDto> DesiredEmployees);