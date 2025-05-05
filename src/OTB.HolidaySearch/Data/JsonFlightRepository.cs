using System.Text.Json;

namespace OTB.HolidaySearch.Data;

public class JsonFlightRepository : IFlightRepository
{
    private readonly List<FlightDataModel> _data;
    
    public JsonFlightRepository(string jsonFilePath)
    {
        var json = File.ReadAllText(jsonFilePath);
        _data = JsonSerializer.Deserialize<List<FlightDataModel>>(json) ?? [];
    }
    
    public IList<FlightDataModel> GetFlights(string departingFrom, string travelingTo, DateOnly departureDate)
    {
        return _data.Where(
                x => x.From == departingFrom && x.To == travelingTo && x.DepartureDate == departureDate)
            .OrderBy(x => x.Price)
            .ToList();
    }
}