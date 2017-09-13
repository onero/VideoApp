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
    [Route("api/Roles")]
    public class RolesController : Controller
    {
        private readonly BLLFacade _bllFacade = new BLLFacade();

        // GET: api/Roles
        [HttpGet]
        public IEnumerable<RoleBO> Get()
        {
            return _bllFacade.RoleService.GetAll();
        }

        // GET: api/Roles/5
        [HttpGet("{id}", Name = "GetRole")]
        public IActionResult Get(int id)
        {
            var genre = _bllFacade.RoleService.GetById(id);

            return genre == null ?
                NotFound(ErrorMessages.IdWasNotFoundMessage(id)) :
                new ObjectResult(genre);
        }
        
        // POST: api/Roles
        [HttpPost]
        public IActionResult Post([FromBody]RoleBO role)
        {
            if (role == null) return BadRequest(ErrorMessages.InvalidJSON);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Created("", _bllFacade.RoleService.Create(role));
        }
        
        // PUT: api/Roles/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]RoleBO role)
        {
            // Validate video is valid JSON
            if (role == null) return BadRequest(ErrorMessages.InvalidJSON);

            // Validate that URL ID matches entity ID
            if (id != role.Id)
                return BadRequest(ErrorMessages.IdDoesNotMatchMessage(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _bllFacade.RoleService.Update(role);

            if (result == null)
                return NotFound(ErrorMessages.IdWasNotFoundMessage(role.Id));

            return Ok("Updated!");
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _bllFacade.RoleService.Delete(id);
            if (!deleted) return NotFound(ErrorMessages.IdWasNotFoundMessage(id));

            return Ok("Deleted");
        }
    }
}
