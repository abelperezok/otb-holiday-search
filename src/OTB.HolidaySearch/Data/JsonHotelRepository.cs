using System.Text.Json;

namespace OTB.HolidaySearch.Data;

public class JsonHotelRepository : IHotelRepository
{
    private readonly List<HotelDataModel> _data;
    
    public JsonHotelRepository(string jsonFilePath)
    {
        var json = File.ReadAllText(jsonFilePath);
        _data = JsonSerializer.Deserialize<List<HotelDataModel>>(json) ?? [];
    }

    public IList<HotelDataModel> GetHotels(DateOnly arrivalDate, uint nights, string localAirport)
    {
        return _data.Where(
                x => x.ArrivalDate == arrivalDate && x.Nights == nights && x.LocalAirports.Contains(localAirport))
            .OrderBy(x => x.PricePerNight)
            .ToList();
    }
}