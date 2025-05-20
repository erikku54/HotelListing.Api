using System.Threading.Tasks;
using AutoMapper;
using HotelListing.Api.Contracts;
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
    private readonly ICountriesRepository _countryRepository;
    private readonly IMapper _mapper;

    public CountriesController(ICountriesRepository repository, IMapper mapper)
    {
        _countryRepository = repository;
        _mapper = mapper;
    }

    // GET: api/Countries
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
    {
        var countries = await _countryRepository.GetAllAsync();
        var results = _mapper.Map<List<GetCountryDto>>(countries);

        return Ok(results);
    }

    // GET: api/Countries/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CountryDto>> GetCountry(int id)
    {
        var country = await _countryRepository.GetDetails(id);

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
        await _countryRepository.AddAsync(country);

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

        var country = await _countryRepository.GetByIdAsync(id);
        if (country == null)
        {
            return NotFound();
        }

        // Map the updated properties from the DTO to the entity
        // 因為country是被tracked的entity，所以不需要再set state
        // EF Core會自動標註為modified，而在SaveChanges時會自動更新
        // _context.Entry(country).State = EntityState.Modified;
        // _mapper.Map(updateCountryDto, country);
        // await _context.SaveChangesAsync();

        _mapper.Map(updateCountryDto, country);

        try
        {
            await _countryRepository.UpdateAsync(country);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await CountryExists(id))
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
        var country = await _countryRepository.GetByIdAsync(id);
        if (country == null)
        {
            return NotFound();
        }

        await _countryRepository.DeleteAsync(id);

        return NoContent();
    }

    private async Task<bool> CountryExists(int id)
    {
        // return _context.Countries.Any(e => e.CountryId == id);
        return await _countryRepository.Exists(id);
    }
}
