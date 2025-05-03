using System.Collections.ObjectModel;
using Moq;
using OTB.HolidaySearch.Data;
using OTB.HolidaySearch.Models;

namespace OTB.HolidaySearch.Tests;

public class HolidaySearchServiceTests
{
    [Fact]
    public void TestSearchEmptyData()
    {
        // Arrange
        var flightRepo = new Mock<IFlightRepository>();
            
        flightRepo
            .Setup(x => x.GetFlights(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateOnly>()))
            .Returns([]);
        
        var service = new HolidaySearchService(flightRepo.Object);
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
    public void TestManchesterMalagaJuly2023For7Nights()
    {
        // Arrange
        var flightRepo = new Mock<IFlightRepository>();
            
        flightRepo
            .Setup(x => x.GetFlights(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateOnly>()))
            .Returns([
                new FlightDataModel
                {
                    Id = 2,
                    From = "MAN",
                    To = "AGP",
                    DepartureDate = new DateOnly(2023, 7, 1),
                }
            ]);
        
        var service = new HolidaySearchService(flightRepo.Object);
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
        Assert.Equal(9, result.Results.First().Hotel.Id);
    }
}
