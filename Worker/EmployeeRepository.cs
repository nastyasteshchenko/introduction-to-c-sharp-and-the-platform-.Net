using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Nsu.Hackathon.Problem.Worker;

public class EmployeeRepository
{
    private const string JuniorResourcesFile = "./Resources/Juniors20.csv";
    private const string TeamLeadsResourcesFile = "./Resources/Teamleads20.csv";

    public List<Employee> Juniors { get; } = ReadEmployeesFromCsvFile(JuniorResourcesFile);
    public List<Employee> TeamLeads { get; } = ReadEmployeesFromCsvFile(TeamLeadsResourcesFile);

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