using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;

namespace VideoRestAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly BLLFacade _bllFacade = new BLLFacade();

        // GET: api/Users
        [HttpGet]
        public IEnumerable<UserBO> Get()
        {
            return _bllFacade.UserService.GetAll();
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var genre = _bllFacade.UserService.GetById(id);

            return genre == null ?
                NotFound(ErrorMessages.IdWasNotFoundMessage(id)) :
                new ObjectResult(genre);
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult Post([FromBody] UserBO user)
        {
            if (user == null) return BadRequest(ErrorMessages.InvalidJSON);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Created("", _bllFacade.UserService.Create(user));
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserBO user)
        {
            // Validate video is valid JSON
            if (user == null) return BadRequest(ErrorMessages.InvalidJSON);

            // Validate that URL ID matches entity ID
            if (id != user.Id)
                return BadRequest(ErrorMessages.IdDoesNotMatchMessage(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _bllFacade.UserService.Update(user);

            if (result == null)
                return NotFound(ErrorMessages.IdWasNotFoundMessage(user.Id));

            return Ok("Updated!");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _bllFacade.UserService.Delete(id);
            if (!deleted) return NotFound(ErrorMessages.IdWasNotFoundMessage(id));

            return Ok("Deleted");
        }
    }
}