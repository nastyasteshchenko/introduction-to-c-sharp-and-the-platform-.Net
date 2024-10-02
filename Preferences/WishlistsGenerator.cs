using System.Runtime.InteropServices;

namespace Nsu.Hackathon.Problem.Preferences;

using Worker;

public static class WishlistsGenerator
{
    public static List<Wishlist> GenerateWishlists(List<Employee> employees, List<Employee> desiredEmployees)
    {
        List<Wishlist> wishlists = [];
        foreach (var employee in employees)
        {
            var desiredEmployeesClone = new List<Employee>(desiredEmployees);
            Random.Shared.Shuffle(CollectionsMarshal.AsSpan(desiredEmployeesClone));
            wishlists.Add(new Wishlist(employee, desiredEmployeesClone));
        }

        return wishlists;
    }
}