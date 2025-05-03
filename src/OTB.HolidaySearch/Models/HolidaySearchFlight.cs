namespace OTB.HolidaySearch.Models;

public class HolidaySearchFlight
{
    public int Id { get; set; }
    public string DepartingFrom { get; set; }
    public string TravelingTo { get; set; }
    public decimal Price { get; set; }
    public string Airline { get; set; }
    public DateOnly DepartureDate { get; set; }
}