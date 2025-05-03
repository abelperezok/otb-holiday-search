using OTB.HolidaySearch.Data;
using OTB.HolidaySearch.Models;

namespace OTB.HolidaySearch;

public class HolidaySearchService
{
    private readonly IFlightRepository _flightRepository;
    private readonly IHotelRepository _hotelRepository;
    
    public HolidaySearchService(IFlightRepository flightRepository, IHotelRepository hotelRepository)
    {
        _flightRepository = flightRepository;
        _hotelRepository = hotelRepository;
    }
    
    public HolidaySearchResults Search(HolidaySearchRequest query)
    {
        var result = new HolidaySearchResults();

        var flights = GetFlights(query.DepartingFrom, query.TravelingTo, query.DepartureDate);

        if (flights.Count == 0)
            return result;

        var hotels = GetHotels(query.DepartureDate, query.Duration, query.TravelingTo);
        
        if (hotels.Count == 0)
            return result;
        
        var count = Math.Min(flights.Count, hotels.Count);
        
        for (var i = 0; i < count; i++)
        {
            var flight = flights[i];
            var hotel = hotels[i];
            result.Results.Add(new HolidaySearchResultItem
            {
                Flight = flight,
                Hotel = hotel
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
    
    
    private List<HolidaySearchHotel> GetHotels(DateOnly arrivalDate, uint nights, string localAirport)
    {
        var hotels = _hotelRepository.GetHotels(arrivalDate, nights, localAirport);
        return hotels.Select(x => new HolidaySearchHotel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.PricePerNight
            })
            .ToList();
    }
}