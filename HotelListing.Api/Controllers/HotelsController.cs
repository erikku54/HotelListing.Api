// using Microsoft.AspNetCore.Http;
using HotelListing.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
    private static readonly List<Hotel> _hotels = new()
    {
        new Hotel
        {
            Id = 1,
            Name = "Hotel One",
            Address = "Address One",
            Rating = 4.5,
        },
        new Hotel
        {
            Id = 2,
            Name = "Hotel Two",
            Address = "Address Two",
            Rating = 4.0,
        },
        new Hotel
        {
            Id = 3,
            Name = "Hotel Three",
            Address = "Address Three",
            Rating = 3.5,
        },
    };

    // GET: api/Hotels
    [HttpGet]
    public ActionResult<IEnumerable<Hotel>> Get()
    {
        return Ok(_hotels);
    }

    // GET: api/Hotels/5
    [HttpGet("{id}")]
    public ActionResult<Hotel> Get(int id)
    {
        var hotel = _hotels.FirstOrDefault(h => h.Id == id);
        if (hotel == null)
        {
            return NotFound();
        }

        return Ok(hotel);
    }

    // POST: api/Hotels
    [HttpPost]
    public ActionResult Post([FromBody] Hotel hotel)
    {
        if (_hotels.Any(h => h.Id == hotel.Id))
        {
            return Conflict("Hotel with the same Id already exists");
        }

        _hotels.Add(hotel);
        return CreatedAtAction(nameof(Get), new { id = hotel.Id }, hotel);
    }

    // PUT: api/Hotels/5
    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] Hotel hotel)
    {
        var existingHotel = _hotels.FirstOrDefault(h => h.Id == id);
        if (existingHotel == null)
        {
            return NotFound();
        }

        existingHotel.Name = hotel.Name;
        existingHotel.Address = hotel.Address;
        existingHotel.Rating = hotel.Rating;

        return NoContent();
    }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var hotel = _hotels.FirstOrDefault(h => h.Id == id);
        if (hotel == null)
        {
            return NotFound(new { Message = $"Hotel with Id {id} not found" });
        }

        _hotels.Remove(hotel);
        return NoContent();
    }
}
