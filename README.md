# Holiday Search Programming Exercise

## Assumptions

* All flights depart and arrive the same day.
* All flights are return flights (or we are ignoring the return flight).
* "The best for the client" means: the cheapest package including both flight and hotel.
* A holiday consists of exactly one flight and one hotel.
* If only flight or only hotel are found, there is no holiday available for the requested destination and date.
* The hotel can be booked only for the exact amount of nights available, not more nights than requested.
* The search library must return multiple resutls (if available) displaying combinations of flight-hotel sorted by lowest total price.

## Remarks

* The main entry point of this library is `HolidaySearchResults.Search` 
* There are three depdencies to create this class:
  - `IFlightRepository` - data access for flights
  - `IHotelRepository` - data access for hotels
  - `IAirportSearchKeyExpander` - handle `Any ... ` cases
* Implementation for all dependency interfaces have been provided.
* Teste coverage is close to 100%