using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Nsu.Hackathon.Problem.Common.Model;

namespace Nsu.Hackathon.Problem.Developer;

public class EmployeeRepository
{
    private const string JuniorResourcesFile = "./Resources/Juniors5.csv";
    private const string TeamLeadsResourcesFile = "./Resources/Teamleads5.csv";

    public List<Employee> Juniors { get; } = ReadEmployeesFromCsvFile(JuniorResourcesFile);
    public List<Employee> TeamLeads { get; } = ReadEmployeesFromCsvFile(TeamLeadsResourcesFile);

    private static List<Employee> ReadEmployeesFromCsvFile(string filePath)
    {
        var reader = new StreamReader(filePath);
        var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";"
        });
        
        List<Employee> employees;
        if (filePath.Equals(JuniorResourcesFile))
        {
            employees = [..csv.GetRecords<Junior>().ToList().Cast<Employee>()];
        }
        else
        {
            employees = [..csv.GetRecords<TeamLead>().ToList().Cast<Employee>()];
        }

        reader.Close();
        return employees;
    }
}