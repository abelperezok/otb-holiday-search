using OTB.HolidaySearch.Data;
using OTB.HolidaySearch.Models;

namespace OTB.HolidaySearch.Tests;

public class HolidaySearchServiceIntegrationTests
{
    private readonly IFlightRepository _flightRepo;
    private readonly IHotelRepository _hotelRepo;
    private readonly IAirportSearchKeyExpander _airportSearchKeyExpander;

    
    public HolidaySearchServiceIntegrationTests()
    {
        _flightRepo = new JsonFlightRepository(JsonFilePathConstants.FlightJsonPath);
        _hotelRepo = new JsonHotelRepository(JsonFilePathConstants.HotelJsonPath);
        _airportSearchKeyExpander = new DefaultAirportSearchKeyExpander();
    }
    
    [Fact]
    public void TestSearchEmptyQuery()
    {
        // Arrange
        var service = new HolidaySearchService(_flightRepo, _hotelRepo, _airportSearchKeyExpander);
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
    
    [Fact]
    public void TestCustomer1ManchesterMalagaJuly2023For7Nights()
    {
        // Arrange
        var service = new HolidaySearchService(_flightRepo, _hotelRepo, _airportSearchKeyExpander);
        var query = new HolidaySearchRequest
        {
            DepartingFrom = "MAN",
            TravelingTo = "AGP",
            DepartureDate = new DateOnly(2023, 7, 1),
            Duration = 7
        };

        // Act
        var result = service.Search(query);

        // Assert
        Assert.Equal(2, result.Results.First().Flight.Id);
        Assert.Equal("MAN", result.Results.First().Flight.DepartingFrom);
        Assert.Equal("AGP", result.Results.First().Flight.TravelingTo);
        Assert.Equal(245, result.Results.First().Flight.Price);
        Assert.Equal("Oceanic Airlines", result.Results.First().Flight.Airline);
        Assert.Equal(new DateOnly(2023, 7, 1), result.Results.First().Flight.DepartureDate);
        Assert.Equal(9, result.Results.First().Hotel.Id);
        Assert.Equal("Nh Malaga", result.Results.First().Hotel.Name);
        Assert.Equal(new DateOnly(2023, 7, 1), result.Results.First().Hotel.ArrivalDate);
        Assert.Equal(7 * 83, result.Results.First().Hotel.Price);
        Assert.Equal("AGP", result.Results.First().Hotel.LocalAirports[0]);
        Assert.Equal(7u, result.Results.First().Hotel.Nights);
        Assert.Equal(826, result.Results.First().TotalPrice);
    }
    
    [Fact]
    public void TestCustomer2AnyLondonAirportMallorca15Jun2023For10Nights()
    {
        // Arrange
        var holidayDate = new DateOnly(2023, 06, 15);
        var service = new HolidaySearchService(_flightRepo, _hotelRepo, _airportSearchKeyExpander);
        var query = new HolidaySearchRequest
        {
            DepartingFrom = HolidaySearchRequestAirports.AnyLondonAirport,
            TravelingTo = "PMI",
            DepartureDate = holidayDate,
            Duration = 10
        };

        // Act
        var result = service.Search(query);

        // Assert
        Assert.Equal(6, result.Results.First().Flight.Id);
        Assert.Equal("LGW", result.Results.First().Flight.DepartingFrom);
        Assert.Equal("PMI", result.Results.First().Flight.TravelingTo);
        Assert.Equal(75, result.Results.First().Flight.Price);
        Assert.Equal("Fresh Airways", result.Results.First().Flight.Airline);
        Assert.Equal(holidayDate, result.Results.First().Flight.DepartureDate);
        Assert.Equal(5, result.Results.First().Hotel.Id);
        Assert.Equal("Sol Katmandu Park & Resort", result.Results.First().Hotel.Name);
        Assert.Equal(holidayDate, result.Results.First().Hotel.ArrivalDate);
        Assert.Equal(10 * 60, result.Results.First().Hotel.Price);
        Assert.Equal("PMI", result.Results.First().Hotel.LocalAirports[0]);
        Assert.Equal(10u, result.Results.First().Hotel.Nights);
        Assert.Equal(10 * 60 + 75, result.Results.First().TotalPrice);
    }
    
    [Fact]
    public void TestCustomer3AnyAirportGranCanaria10Nov2022For14Nights()
    {
        // Arrange
        var holidayDate = new DateOnly(2022, 11, 10);
        var service = new HolidaySearchService(_flightRepo, _hotelRepo, _airportSearchKeyExpander);
        var query = new HolidaySearchRequest
        {
            DepartingFrom = HolidaySearchRequestAirports.AnyAirport,
            TravelingTo = "LPA",
            DepartureDate = holidayDate,
            Duration = 14
        };

        // Act
        var result = service.Search(query);

        // Assert
        Assert.Equal(7, result.Results.First().Flight.Id);
        Assert.Equal("MAN", result.Results.First().Flight.DepartingFrom);
        Assert.Equal("LPA", result.Results.First().Flight.TravelingTo);
        Assert.Equal(125, result.Results.First().Flight.Price);
        Assert.Equal("Trans American Airlines", result.Results.First().Flight.Airline);
        Assert.Equal(holidayDate, result.Results.First().Flight.DepartureDate);
        Assert.Equal(6, result.Results.First().Hotel.Id);
        Assert.Equal("Club Maspalomas Suites and Spa", result.Results.First().Hotel.Name);
        Assert.Equal(holidayDate, result.Results.First().Hotel.ArrivalDate);
        Assert.Equal(14 * 75, result.Results.First().Hotel.Price);
        Assert.Equal("LPA", result.Results.First().Hotel.LocalAirports[0]);
        Assert.Equal(14u, result.Results.First().Hotel.Nights);
        Assert.Equal(14 * 75 + 125, result.Results.First().TotalPrice);
    }
}
