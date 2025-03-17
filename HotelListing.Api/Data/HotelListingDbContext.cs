using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Data;

public class HotelListingDbContext : DbContext
{
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Country> Countries { get; set; }

    public HotelListingDbContext(DbContextOptions<HotelListingDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Country>().HasData(
            new Country
            {
                Id = 1,
                Name = "Jamaica",
                ShortName = "JM"
            },
            new Country
            {
                Id = 2,
                Name = "Bahamas",
                ShortName = "BS"
            },
            new Country
            {
                Id = 3,
                Name = "Cayman Islands",
                ShortName = "CI"
            }
        );

        modelBuilder.Entity<Hotel>().HasData(
            new Hotel
            {
                Id = 1,
                Name = "Sandals Resort and Spa",
                Address = "Negril",
                Rating = 4.5,
                CountryId = 1
            },
            new Hotel
            {
                Id = 2,
                Name = "Grand Palladium",
                Address = "Nassau",
                Rating = 4.0,
                CountryId = 2
            },
            new Hotel
            {
                Id = 3,
                Name = "Ritz Carlton",
                Address = "Grand Cayman",
                Rating = 4.5,
                CountryId = 3
            }
        );
    }
}
