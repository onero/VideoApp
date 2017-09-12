using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;

namespace VideoRestAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProfilesController : Controller
    {
        private readonly BLLFacade _bllFacade = new BLLFacade();

        // GET: api/Profiles
        [HttpGet]
        public IEnumerable<ProfileBO> Get()
        {
            return new List<ProfileBO>(_bllFacade.ProfileService.GetAll());
        }

        // GET: api/Profiles/5
        [HttpGet("{id}", Name = "GetProfile")]
        public IActionResult Get(int id)
        {
            var video = _bllFacade.ProfileService.GetById(id);

            return video == null ? 
                NotFound(ErrorMessages.IdWasNotFoundMessage(id)) : 
                new ObjectResult(video);
        }

        // POST: api/Profiles
        [HttpPost]
        public IActionResult Post([FromBody] ProfileBO profile)
        {
            if (profile == null) return BadRequest(ErrorMessages.InvalidJSON);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Created("", _bllFacade.ProfileService.Create(profile));
        }

        // PUT: api/Profiles/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProfileBO profile)
        {
            // Validate that profile is valid JSON
            if (profile == null) return BadRequest(ErrorMessages.InvalidJSON);

            // Validate that URL ID matches entity ID
            if (id != profile.Id)
                return BadRequest(ErrorMessages.IdDoesNotMatchMessage(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _bllFacade.ProfileService.Update(profile);

            if (result == null)
                return NotFound(ErrorMessages.IdWasNotFoundMessage(profile.Id));

            return Ok("Updated");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _bllFacade.ProfileService.Delete(id);
            if (!deleted) return NotFound(ErrorMessages.IdWasNotFoundMessage(id));
            return Ok("Deleted");
        }
    }
}