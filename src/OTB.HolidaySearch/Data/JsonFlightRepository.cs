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
    
    public IList<FlightDataModel> GetFlights(string[]? departingFrom, string travelingTo, DateOnly departureDate)
    {
        var query = _data.Where(x => x.To == travelingTo && x.DepartureDate == departureDate);

        if (departingFrom is not null && departingFrom.Length > 0)
        {
            query = query.Where(x => departingFrom.Contains(x.From));
        }
            
        return query.OrderBy(x => x.Price)
            .ToList();
    }
}