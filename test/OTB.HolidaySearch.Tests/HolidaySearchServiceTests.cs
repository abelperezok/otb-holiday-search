using OTB.HolidaySearch.Models;

namespace OTB.HolidaySearch.Tests;

public class HolidaySearchServiceTests
{
    [Fact]
    public void TestSearch_EmptyData()
    {
        // Arrange
        var service = new HolidaySearchService();
        var query = new HolidaySearchRequest
        {
            DepartingFrom = string.Empty,
            TravelingTo = string.Empty,
            DepartureDate = DateOnly.MinValue,
            Duration = 0
        };
        
        // Act
        var result = service.Search(query);
        
        // Assert
        Assert.NotNull(result);
        Assert.Empty(result.Results);
    }
}
