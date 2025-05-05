using OTB.HolidaySearch.Data;
using OTB.HolidaySearch.Models;

namespace OTB.HolidaySearch;

public class HolidaySearchService
{
    private readonly IFlightRepository _flightRepository;
    private readonly IHotelRepository _hotelRepository;
    private readonly IAirportSearchKeyExpander _airportSearchKeyExpander;
    private const int MaxFlights = 10;
    private const int MaxHotels = 10;

    public HolidaySearchService(
        IFlightRepository flightRepository, 
        IHotelRepository hotelRepository, 
        IAirportSearchKeyExpander airportSearchKeyExpander)
    {
        _flightRepository = flightRepository;
        _hotelRepository = hotelRepository;
        _airportSearchKeyExpander = airportSearchKeyExpander;
    }

    public HolidaySearchResults Search(HolidaySearchRequest query)
    {
        var result = new HolidaySearchResults();
        
        var departingFrom = _airportSearchKeyExpander.ExpandAirportList(query.DepartingFrom);

        var flights = GetFlights(departingFrom, query.TravelingTo, query.DepartureDate);

        if (flights.Count == 0)
            return result;

        var hotels = GetHotels(query.DepartureDate, query.Duration, query.TravelingTo);

        if (hotels.Count == 0)
            return result;

        result.Results = MergeResults(flights, hotels);

        return result;
    }

    private List<HolidaySearchFlight> GetFlights(string[] departingFrom, string travelingTo, DateOnly departureDate)
    {
        var flights = _flightRepository.GetFlights(departingFrom, travelingTo, departureDate, MaxFlights);
        return flights.Select(x => new HolidaySearchFlight
            {
                Id = x.Id,
                DepartingFrom = x.From,
                TravelingTo = x.To,
                Price = x.Price,
                DepartureDate = x.DepartureDate,
                Airline = x.Airline
            })
            .ToList();
    }

    private List<HolidaySearchHotel> GetHotels(DateOnly arrivalDate, uint nights, string localAirport)
    {
        var hotels = _hotelRepository.GetHotels(arrivalDate, nights, localAirport, MaxHotels);
        return hotels.Select(x => new HolidaySearchHotel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.PricePerNight * x.Nights,
                ArrivalDate = x.ArrivalDate,
                LocalAirports = x.LocalAirports,
                Nights = x.Nights
            })
            .ToList();
    }

    private static List<HolidaySearchResultItem> MergeResults(List<HolidaySearchFlight> flights, List<HolidaySearchHotel> hotels)
    {
        var result = new List<HolidaySearchResultItem>();
        
        foreach (var flight in flights)
        {
            foreach (var hotel in hotels)
            {
                result.Add(new HolidaySearchResultItem
                {
                    Flight = flight,
                    Hotel = hotel,
                    TotalPrice = flight.Price + hotel.Price,
                });
            }
        }

        return result.OrderBy(x => x.TotalPrice).ToList();
    }
}
