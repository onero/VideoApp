using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VideoRestAPI.Controllers
{
    [Route("api/[controller]")]
    public class GenresController : Controller
    {
        private readonly BLLFacade _bllFacade = new BLLFacade();

        // GET: api/values
        [HttpGet]
        public IEnumerable<GenreBO> Get()
        {
            return _bllFacade.GenreService.GetAll();

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var genre = _bllFacade.GenreService.GetById(id);

            return genre == null ?
                NotFound(ErrorMessages.IdWasNotFoundMessage(id)) :
                new ObjectResult(genre);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]GenreBO genre)
        {
            if (genre == null) return BadRequest(ErrorMessages.InvalidJSON);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Created("", _bllFacade.GenreService.Create(genre));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]GenreBO genre)
        {
            // Validate video is valid JSON
            if (genre == null) return BadRequest(ErrorMessages.InvalidJSON);

            // Validate that URL ID matches entity ID
            if (id != genre.Id)
                return BadRequest(ErrorMessages.IdDoesNotMatchMessage(id));

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _bllFacade.GenreService.Update(genre);

            if (result == null)
                return NotFound(ErrorMessages.IdWasNotFoundMessage(genre.Id));

            return Ok("Updated!");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _bllFacade.GenreService.Delete(id);
            if (!deleted) return NotFound(ErrorMessages.IdWasNotFoundMessage(id));

            return Ok("Deleted");
        }
    }
}
