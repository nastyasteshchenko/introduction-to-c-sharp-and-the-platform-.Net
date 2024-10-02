namespace Nsu.Hackathon.Problem;

using Worker;
using Manager;

public static class MainProgram
{
    private const int Times = 1000;

    public static void Main(string[] args)
    {
        var employeeRepository = LoadEmployeeRepository();
        if (employeeRepository == null)
        {
            return;
        }

        var hackathonManager = new HackathonManager(employeeRepository);
        hackathonManager.StartHackathonCertainTimes(Times);
    }

    private static EmployeeRepository? LoadEmployeeRepository()
    {
        try
        {
            return EmployeeRepository.Load();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while loading employee repository. " + e.Message);
            return null;
        }
    }
}