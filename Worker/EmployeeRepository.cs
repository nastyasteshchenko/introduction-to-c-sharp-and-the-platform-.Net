using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Nsu.Hackathon.Problem.Worker;

public class EmployeeRepository
{
    private const string JuniorResourcesFile = "./Resources/Juniors20.csv";
    private const string TeamLeadsResourcesFile = "./Resources/Teamleads20.csv";

    public List<Employee> Juniors { get; }
    public List<Employee> TeamLeads { get; }

    private EmployeeRepository(List<Employee> juniors, List<Employee> teamLeads)
    {
        Juniors = juniors;
        TeamLeads = teamLeads;
    }

    public static EmployeeRepository Load()
    {
        var juniors = ReadEmployeesFromCsvFile(JuniorResourcesFile);
        var teamLeads = ReadEmployeesFromCsvFile(TeamLeadsResourcesFile);
        return new EmployeeRepository(juniors, teamLeads);
    }

    private static List<Employee> ReadEmployeesFromCsvFile(string filePath)
    {
        var reader = new StreamReader(filePath);
        var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";"
        });
        var employees = csv.GetRecords<Employee>().ToList();
        reader.Close();
        return employees;
    }
}