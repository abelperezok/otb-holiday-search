namespace OTB.HolidaySearch.Data;

public interface IHotelRepository
{
    IList<HotelDataModel> GetHotels(DateOnly arrivalDate, uint nights, string localAirport);
}
