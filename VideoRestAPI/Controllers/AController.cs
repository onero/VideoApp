using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;

namespace VideoRestAPI.Controllers
{
    public abstract class AController<TEntity> : Controller
    {
        protected IService<TEntity> Service;

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
        public abstract IActionResult Put(int id, [FromBody] TEntity entity);

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