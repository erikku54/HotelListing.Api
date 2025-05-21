using System.ComponentModel.DataAnnotations;

namespace HotelListing.Api.Models.Hotel;

public class BaseHotelDto
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Address { get; set; } = null!;
    public double? Rating { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "CountryId must be greater than 0")]
    public int CountryId { get; set; }
}
