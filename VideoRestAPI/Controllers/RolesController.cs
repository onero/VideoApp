using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;

namespace VideoRestAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Roles")]
    public class RolesController : AController<RoleBO>
    {
        public RolesController()
        {
            Service = new BLLFacade().RoleService;
        }

        public override IActionResult Put(int id, [FromBody]RoleBO entity)
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
