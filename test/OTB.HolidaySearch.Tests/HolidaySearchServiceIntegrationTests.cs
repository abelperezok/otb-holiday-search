using OTB.HolidaySearch.Data;
using OTB.HolidaySearch.Models;

namespace OTB.HolidaySearch.Tests;

public class HolidaySearchServiceIntegrationTests
{
    private readonly IFlightRepository _flightRepo;
    private readonly IHotelRepository _hotelRepo;
    private const string FlightJsonPath = "../../../../data/flights.json";
    private const string HotelJsonPath = "../../../../data/hotels.json";
    
    public HolidaySearchServiceIntegrationTests()
    {
        _flightRepo = new JsonFlightRepository(FlightJsonPath);
        _hotelRepo = new JsonHotelRepository(HotelJsonPath);
    }
    
    [Fact]
    public void TestSearchEmptyQuery()
    {
        // Arrange
        var service = new HolidaySearchService(_flightRepo, _hotelRepo);
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
