namespace OTB.HolidaySearch.Data;

public interface IFlightRepository
{
    IList<FlightDataModel> GetFlights(string? departingFrom, string travelingTo, DateOnly departureDate);
}