using System.Runtime.InteropServices;

namespace Nsu.Hackathon.Problem.Preferences;

using Worker;

public static class PreferencesGenerator
{
    public static List<Preference> GeneratePreferences(List<Employee> employees, List<Employee> desiredEmployees)
    {
        List<Preference> wishlists = [];
        foreach (var employee in employees)
        {
            var desiredEmployeesClone = new List<Employee>(desiredEmployees);
            Random.Shared.Shuffle(CollectionsMarshal.AsSpan(desiredEmployeesClone));
            wishlists.Add(new Preference(employee, desiredEmployeesClone));
        }

        return wishlists;
    }
}