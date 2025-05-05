using OTB.HolidaySearch.Data;
using OTB.HolidaySearch.Models;

namespace OTB.HolidaySearch.Tests;

public class JsonFlightRepositoryTests
{
    [Fact]
    public void TestCreateWithValidFilePath()
    {
        // Arrange

        // Act
        var repo = new JsonFlightRepository(JsonFilePathConstants.FlightJsonPath);

        // Assert
        Assert.NotNull(repo);
    }
    
    [Fact]
    public void TestCreateWithInvalidFilePath()
    {
        // Arrange

        // Act
        var ex = Assert.Throws<InvalidOperationException>(() =>
        {
            var repo = new JsonFlightRepository("invalid-path");
        });

        // Assert
        Assert.IsType<FileNotFoundException>(ex.InnerException);
    }
}