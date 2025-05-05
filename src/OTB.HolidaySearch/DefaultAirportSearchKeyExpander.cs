using OTB.HolidaySearch.Models;

namespace OTB.HolidaySearch;

public class DefaultAirportSearchKeyExpander : IAirportSearchKeyExpander
{
    public string[] ExpandAirportList(string searchKey)
    {
        return searchKey switch
        {
            HolidaySearchRequestAirports.AnyAirport => [],
            HolidaySearchRequestAirports.AnyLondonAirport => ["LCY", "LHR", "LGW", "LTN", "STN", "SEN"],
            HolidaySearchRequestAirports.AnyNewYorkAirport => ["NYC", "LGA", "SWF", "NYS", "JFK", "EWR"],
            _ => [searchKey]
        };
    }
}
