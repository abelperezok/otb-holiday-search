using System.Text.Json;

namespace OTB.HolidaySearch.Data;

public class JsonHotelRepository : IHotelRepository
{
    private readonly List<HotelDataModel> _data;
    
    public JsonHotelRepository(string jsonFilePath)
    {
        try
        {
            var json = File.ReadAllText(jsonFilePath);
            var data = JsonSerializer.Deserialize<List<HotelDataModel>>(json);
            _data = data ?? throw new JsonException("Invalid json content - null data");
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

    public IList<HotelDataModel> GetHotels(DateOnly arrivalDate, uint nights, string localAirport, int maximumResutls)
    {
        return _data.Where(
                x => x.ArrivalDate == arrivalDate && x.Nights == nights && x.LocalAirports.Contains(localAirport))
            .OrderBy(x => x.PricePerNight)
            .Take(maximumResutls)
            .ToList();
    }
}