using OTB.HolidaySearch.Data;
using OTB.HolidaySearch.Models;

namespace OTB.HolidaySearch;

public class HolidaySearchService
{
    private readonly IFlightRepository _flightRepository;
    
    public HolidaySearchService(IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }
    
    public HolidaySearchResults Search(HolidaySearchRequest query)
    {
        var result = new HolidaySearchResults();

        var flights = GetFlights(query.DepartingFrom, query.TravelingTo, query.DepartureDate);

        if (flights.Count == 0)
            return result;

        for (var i = 0; i < flights.Count; i++)
        {
            var flight = flights[i];
            result.Results.Add(new HolidaySearchResultItem
            {
                Flight = flight,
            });
        }

        return result;
    }
    
    
    
    
    private List<HolidaySearchFlight> GetFlights(string departingFrom, string travelingTo, DateOnly departureDate)
    {
        var flights = _flightRepository.GetFlights(departingFrom, travelingTo, departureDate);
        return flights.Select(x => new HolidaySearchFlight
            {
                Id = x.Id,
                DepartingFrom = x.From,
                TravelingTo = x.To,
                Price = x.Price
            })
            .ToList();
    }
}