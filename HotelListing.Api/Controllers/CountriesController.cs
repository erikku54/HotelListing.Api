using HotelListing.Api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly HotelListingDbContext _context;

    public CountriesController(HotelListingDbContext context)
    {
        _context = context;
    }

    // GET: api/Countries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
    {
        var countries = await _context.Countries.ToListAsync();
        return Ok(countries);
    }

    // GET: api/Countries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Country>> GetCountry(int id)
    {
        var country = await _context.Countries.FindAsync(id);

        if (country == null)
        {
            return NotFound();
        }

        return Ok(country);
    }

    // POST: api/Countries
    [HttpPost]
    public async Task<ActionResult<Country>> PostCountry(Country country)
    {
        _context.Countries.Add(country);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCountry), new { id = country.CountryId }, country);
    }

    // PUT: api/Countries/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCountry(int id, Country country)
    {
        if (id != country.CountryId)
        {
            return BadRequest();
        }

        _context.Entry(country).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CountryExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Countries/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        var country = await _context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
        }

        _context.Countries.Remove(country);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CountryExists(int id)
    {
        return _context.Countries.Any(e => e.CountryId == id);
    }
}
