using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VideoRestAPI.Controllers
{
    [Route("api/[controller]")]
    public class RentalsController : AController<RentalBO>
    {
        public RentalsController(IRentalService service)
        {
            Service = service;
        }

        public override IActionResult Put(int id, [FromBody]RentalBO entity)
        {
            // Validate TEntity is valid JSON
            if (entity == null) return BadRequest(ErrorMessages.InvalidJSON);

            // Validate that URL ID matches entity ID
            if (id != entity.Id)
                return BadRequest(ErrorMessages.IdDoesNotMatchMessage(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = Service.Update(entity);

            if (result == null)
                return NotFound(ErrorMessages.IdWasNotFoundMessage(id));

            return Ok("Updated!");
        }
    }
}