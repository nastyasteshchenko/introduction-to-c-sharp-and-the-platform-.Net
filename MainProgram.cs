namespace Nsu.Hackathon.Problem;

public static class MainProgram
{
    private const int Times = 1000;

    public static void Main(string[] args)
    {
        var employeeRepository = EmployeeRepository.Load();
        var hackathonStarter = new HackathonManager(employeeRepository);
        hackathonStarter.StartCertainTimes(Times);
    }
}