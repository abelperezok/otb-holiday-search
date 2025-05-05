using System.Text.Json;

namespace OTB.HolidaySearch.Data;

public class JsonFlightRepository : IFlightRepository
{
    private readonly List<FlightDataModel> _data;
    
    public JsonFlightRepository(string jsonFilePath)
    {
        try
        {
            var json = File.ReadAllText(jsonFilePath);
            _data = JsonSerializer.Deserialize<List<FlightDataModel>>(json) ?? [];
        }
        catch (FileNotFoundException ex)
        {
            throw new InvalidOperationException("Unable to load json file", ex);
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException("Invalid json content", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unknown error loading json file", ex);
        }
    }
    
    public IList<FlightDataModel> GetFlights(string[]? departingFrom, string travelingTo, DateOnly departureDate, int maximumResutls)
    {
        var query = _data.Where(x => x.To == travelingTo && x.DepartureDate == departureDate);

        if (departingFrom is not null && departingFrom.Length > 0)
        {
            query = query.Where(x => departingFrom.Contains(x.From));
        }
            
        return query.OrderBy(x => x.Price)
            .Take(maximumResutls)
            .ToList();
    }
}