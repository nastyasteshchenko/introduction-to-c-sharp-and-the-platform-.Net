using Nsu.Hackathon.Problem.Common.Dto;
using Nsu.Hackathon.Problem.Common.Model;

namespace Nsu.Hackathon.Problem.Common.Mapper;

public class EmployeeMapper
{
    public List<EmployeeDto> EmployeeToEmployeeDto(List<Employee> employees)
    {
        return employees.Select(EmployeeToEmployeeDto).ToList();
    }

    public EmployeeDto EmployeeToEmployeeDto(Employee employee)
    {
        var type = employee is Junior ? "Junior" : "TeamLead";
        return new EmployeeDto(type, employee.Id, employee.Name);
    }

    public List<Employee> EmployeeDtoToEmployee(List<EmployeeDto> employeesDtos)
    {
        return employeesDtos.Select(EmployeeDtoToEmployee).ToList();
    }

    public Employee EmployeeDtoToEmployee(EmployeeDto employeeDto)
    {
        if (employeeDto.Type == "Junior")
        {
            return new Junior
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
            };
        }

        return new TeamLead
        {
            Id = employeeDto.Id,
            Name = employeeDto.Name
        };
    }
}