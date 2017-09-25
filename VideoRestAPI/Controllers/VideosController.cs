using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using VideoAppDAL;

namespace VideoRestAPI.Controllers
{
    [Route("api/[controller]")]
    public class VideosController : AController<VideoBO>
    {
        public VideosController(IVideoService service)
        {
            Service = service;
        }

        [HttpPut("{id}")]
        public override IActionResult Put(int id, [FromBody] VideoBO entity)
        {
            // Validate TEntity is valid JSON
            if (entity == null) return BadRequest(ErrorMessages.InvalidJSON);

            //Validate that URL ID matches entity ID
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