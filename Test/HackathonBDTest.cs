using Microsoft.EntityFrameworkCore;
using Nsu.Hackathon.Problem;

namespace Test;

public class HackathonBDTest
{
    private HackathonContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<HackathonContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new HackathonContext(options);
    }

    // [Fact]
    // public async Task Should_Add_Hackathon_To_Database()
    // {
    //     using (var context = CreateInMemoryContext())
    //     {
    //         var service = new HackathonWorker(context);
    //         var hackathonId = await service.ConductHackathonAsync("Test Hackathon");
    //
    //         var hackathon = await context.Hackathons.FindAsync(hackathonId);
    //         Assert.NotNull(hackathon);
    //         Assert.Equal("Test Hackathon", hackathon.Name);
    //     }
    // }
    //
    // [Fact]
    // public async Task Should_Calculate_Average_Harmony()
    // {
    //     using (var context = CreateInMemoryContext())
    //     {
    //         // Arrange: Создайте хакатоны с командами и гармоничностью
    //         // Act: Посчитайте среднюю гармоничность
    //         var avgHarmony = await service.CalculateAverageHarmonyAsync();
    //
    //         // Assert: Убедитесь, что значение корректное
    //         Assert.True(avgHarmony > 0);
    //     }
    // }
}