using AutoMapper;
using HotelListing.Api.Data;
using HotelListing.Api.Models.Country;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace HotelListing.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly HotelListingDbContext _context;
    private readonly IMapper _mapper;

    public CountriesController(HotelListingDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/Countries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
    {
        var countries = await _context.Countries.ToListAsync();
        var results = _mapper.Map<List<GetCountryDto>>(countries);

        return Ok(results);
    }

    // GET: api/Countries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CountryDto>> GetCountry(int id)
    {
        var country = await _context
            .Countries.Include(c => c.Hotels)
            .FirstOrDefaultAsync(c => c.CountryId == id);

        if (country == null)
        {
            return NotFound();
        }

        var result = _mapper.Map<CountryDto>(country);

        return Ok(country);
    }

    // POST: api/Countries
    [HttpPost]
    public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountry)
    {
        var country = _mapper.Map<Country>(createCountry);

        // Add the new country to the database
        _context.Countries.Add(country);
        await _context.SaveChangesAsync();

        // Return the created country with a 201 Created status code
        return CreatedAtAction(nameof(GetCountry), new { id = country.CountryId }, country);
    }

    // PUT: api/Countries/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
    {
        if (id != updateCountryDto.Id)
        {
            return BadRequest();
        }

        // Check if the country exists
        var country = await _context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
        }

        // Map the updated properties from the DTO to the entity
        // 因為country是被tracked的entity，所以不需要再set state
        // EF Core會自動標註為modified，而在SaveChanges時會自動更新
        // _context.Entry(country).State = EntityState.Modified;
        _mapper.Map(updateCountryDto, country);

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
