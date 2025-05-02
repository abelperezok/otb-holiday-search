namespace OTB.HolidaySearch.Models;

public class HolidaySearchResultItem
{
    public decimal TotalPrice { get; set; }
    public HolidaySearchFlight Flight { get; set; }
    public HolidaySearchHotel Hotel { get; set; }
}
