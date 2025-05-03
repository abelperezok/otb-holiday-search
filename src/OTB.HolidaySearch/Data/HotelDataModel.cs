using System.Text.Json.Serialization;

namespace OTB.HolidaySearch.Data;

public class HotelDataModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("arrival_date")]
    public DateOnly ArrivalDate { get; set; }
    
    [JsonPropertyName("price_per_night")]
    public decimal PricePerNight { get; set; }

    [JsonPropertyName("local_airports")]
    public List<string> LocalAirports { get; set; } = [];
    
    [JsonPropertyName("nights")]
    public int Nights { get; set; }
}
