namespace OTB.HolidaySearch.Models;

public class HolidaySearchHotel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly ArrivalDate { get; set; }
    public decimal Price { get; set; }
    public List<string> LocalAirports { get; set; } = [];
    public uint Nights { get; set; }
}
