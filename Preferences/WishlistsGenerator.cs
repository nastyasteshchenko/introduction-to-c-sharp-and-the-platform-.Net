using System.Runtime.InteropServices;

namespace Nsu.Hackathon.Problem.Preferences;

using Worker;

public static class WishlistsGenerator
{
    public static List<WishlistEntity> GenerateWishlists(List<EmployeeEntity> employees, List<EmployeeEntity> desiredEmployees)
    {
        List<WishlistEntity> wishlists = [];
        foreach (var employee in employees)
        {
            var desiredEmployeesClone = new List<EmployeeEntity>(desiredEmployees);
            Random.Shared.Shuffle(CollectionsMarshal.AsSpan(desiredEmployeesClone));
            var employeeWishlist = desiredEmployeesClone.Select(e =>
            {
                var wishlist = new WishlistEntity()
                {
                    Employee = employee,
                    DesiredEmployee = e
                };
                return wishlist;
            }).ToList();
            wishlists.AddRange(employeeWishlist);
        }

        return wishlists;
    }
}