using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VideoRestAPI.Controllers
{
    [Route("api/[controller]")]
    public class RentalsController : Controller
    {
        private readonly BLLFacade _bllFacade = new BLLFacade();

        // GET: api/rentals
        [HttpGet]
        public IEnumerable<RentalBO> Get()
        {
            return new List<RentalBO>(_bllFacade.RentalService.GetAll());
        }

        // GET api/rentals/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var rental = _bllFacade.RentalService.GetById(id);

            return rental == null ?
                NotFound(ErrorMessages.IdWasNotFoundMessage(id)) :
                new ObjectResult(rental);
        }

        // POST api/rentals
        [HttpPost]
        public IActionResult Post([FromBody] RentalBO rental)
        {
            if (rental == null) return BadRequest(ErrorMessages.InvalidJSON);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Created("", _bllFacade.RentalService.Create(rental));
        }

        // PUT api/ralues/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RentalBO rental)
        {
            // Validate that profile is valid JSON
            if (rental == null) return BadRequest(ErrorMessages.InvalidJSON);

            // Validate that URL ID matches entity ID
            if (id != rental.Id)
                return BadRequest(ErrorMessages.IdDoesNotMatchMessage(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _bllFacade.RentalService.Update(rental);

            if (result == null)
                return NotFound(ErrorMessages.IdWasNotFoundMessage(rental.Id));

            return Ok("Updated");
        }

        // DELETE api/ralues/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _bllFacade.RentalService.Delete(id);
            if (!deleted) return NotFound(ErrorMessages.IdWasNotFoundMessage(id));
            return Ok("Deleted");
        }
    }
}