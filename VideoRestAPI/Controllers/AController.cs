using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;

namespace VideoRestAPI.Controllers
{
    public abstract class AController<TEntity> : Controller
    {
        protected readonly IService<TEntity> Service;

        protected AController(IService<TEntity> service)
        {
            Service = service;
        }

        // GET api/TEntity
        [HttpGet]
        public IEnumerable<TEntity> Get()
        {
            return Service.GetAll();
        }

        // GET api/TEntity/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var video = Service.GetById(id);

            return video == null ?
                NotFound(ErrorMessages.IdWasNotFoundMessage(id)) :
                new ObjectResult(video);
        }

        // POST api/TEntity
        [HttpPost]
        public IActionResult Post([FromBody] TEntity entity)
        {
            if (entity == null) return BadRequest(ErrorMessages.InvalidJSON);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Created("", Service.Create(entity));
        }


        // PUT api/TEntity/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TEntity entity)
        {
            // Validate TEntity is valid JSON
            if (entity == null) return BadRequest(ErrorMessages.InvalidJSON);

            // Validate that URL ID matches entity ID
            //if (id != entity.Id)
            //    return BadRequest(ErrorMessages.IdDoesNotMatchMessage(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = Service.Update(entity);

            if (result == null)
                return NotFound(ErrorMessages.IdWasNotFoundMessage(id));

            return Ok("Updated!");
        }

        // DELETE api/TEntity/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = Service.Delete(id);
            if (!deleted) return NotFound(ErrorMessages.IdWasNotFoundMessage(id));

            return Ok("Deleted");
        }
    }
}