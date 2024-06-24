using PABlabZalApi.Core.Entities;
using PABlabZalApi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PABlabZalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rental>>> GetRentals()
        {
            var rentals = await _rentalService.GetRentalsAsync();
            return Ok(rentals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rental>> GetRental(int id)
        {
            var rental = await _rentalService.GetRentalByIdAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            return Ok(rental);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRental([FromBody] Rental rental)
        {
            await _rentalService.AddRentalAsync(rental);
            return CreatedAtAction(nameof(GetRental), new { id = rental.Id }, rental);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRental(int id, [FromBody] Rental rental)
        {
            if (id != rental.Id)
            {
                return BadRequest();
            }

            await _rentalService.UpdateRentalAsync(rental);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRental(int id)
        {
            await _rentalService.DeleteRentalAsync(id);
            return NoContent();
        }
    }
}