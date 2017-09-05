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
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var video = _bllFacade.ProfileService.GetById(id);

            if (video == null) return NotFound();

            return new ObjectResult(video);
        }
        
        // POST: api/Profiles
        [HttpPost]
        public IActionResult Post([FromBody]ProfileBO profile)
        {
            if (profile == null) return BadRequest();

            var createdProfile = _bllFacade.ProfileService.Create(profile);
            return Created("", createdProfile);
        }
        
        // PUT: api/Profiles/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ProfileBO profile)
        {
            if (id != profile.Id) return BadRequest();

            var result = _bllFacade.ProfileService.Update(profile);

            if (result == null) return NotFound();

            return Ok();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _bllFacade.ProfileService.Delete(id);
            if (!deleted) return NotFound();
            return Ok();
        }
    }
}
