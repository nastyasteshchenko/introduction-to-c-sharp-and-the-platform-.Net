using System.Runtime.InteropServices;
using Common.Model;

namespace Developer;

public static class PreferencesGenerator
{
    public static List<Preference> GeneratePreferences(List<Employee> employees, List<Employee> desiredEmployees)
    {
        List<Preference> wishlists = [];
        wishlists.AddRange(employees.Select(employee => GeneratePreference(employee, desiredEmployees)));

        return wishlists;
    }

    public static Preference GeneratePreference(Employee employee, List<Employee> desiredEmployees)
    {
        var desiredEmployeesClone = new List<Employee>(desiredEmployees);
        Random.Shared.Shuffle(CollectionsMarshal.AsSpan(desiredEmployeesClone));
        return new Preference(employee, desiredEmployeesClone);
    }
}