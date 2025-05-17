using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.Models.Country;

public class CreateCountryDto
{
    [Required]
    public string Name { get; set; } = null!;
    public string ShortName { get; set; } = null!;
}
