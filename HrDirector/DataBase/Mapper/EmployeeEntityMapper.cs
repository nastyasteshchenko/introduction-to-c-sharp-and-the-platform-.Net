using Common.Model;
using HrDirector.DataBase.Model.Employee;

namespace HrDirector.DataBase.Mapper;

public class EmployeeEntityMapper(HackathonContext hackathonContext)
{
    private readonly List<EmployeeEntity> _mappedEntities = [];

    public List<EmployeeEntity> EmployeeToEmployeeEntity(List<Employee> employees)
    {
        return employees.Select(EmployeeToEmployeeEntity).ToList();
    }

    public EmployeeEntity EmployeeToEmployeeEntity(Employee employee)
    {
        var entity = hackathonContext.Employees.SingleOrDefault(e => e.Id == employee.Id);
        if (entity != null)
        {
            return entity;
        }

        entity = _mappedEntities.SingleOrDefault(e => e.Id == employee.Id);
        if (entity != null)
        {
            return entity;
        }

        entity = employee is Junior
            ? new JuniorEntity
            {
                Id = employee.Id,
                Name = employee.Name
            }
            : new TeamLeadEntity
            {
                Id = employee.Id,
                Name = employee.Name
            };
        _mappedEntities.Add(entity);
        return entity;
    }
}