using HotelListing.Api.Models.Hotel;

namespace HotelListing.Api.Models.Country;

public class CountryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ShortName { get; set; } = null!;

    public List<HotelDto> Hotels { get; set; } = new List<HotelDto>();
}
