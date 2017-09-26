﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;

namespace VideoRestAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProfilesController : AController<ProfileBO>
    {
        public ProfilesController(IProfileService service)
        {
            Service = service;
        }

        public override IActionResult Put(int id, [FromBody]ProfileBO entity)
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