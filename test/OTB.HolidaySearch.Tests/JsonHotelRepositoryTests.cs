using OTB.HolidaySearch.Data;

namespace OTB.HolidaySearch.Tests;

public class JsonHotelRepositoryTests
{
    [Fact]
    public void TestCreateWithValidFilePath()
    {
        // Arrange

        // Act
        var repo = new JsonHotelRepository(JsonFilePathConstants.HotelJsonPath);

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
