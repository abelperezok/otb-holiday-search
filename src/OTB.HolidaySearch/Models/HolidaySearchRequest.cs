namespace OTB.HolidaySearch.Models;

public class HolidaySearchRequest
{
    public string DepartingFrom { get; set; }
    public string TravelingTo { get; set; }
    public DateOnly DepartureDate { get; set; }
    public uint Duration { get; set; }
}
