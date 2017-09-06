using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;

namespace VideoRestAPI.Controllers
{
    [Route("api/[controller]")]
    public class VideosController : Controller
    {
        private readonly BLLFacade _bllFacade = new BLLFacade();

        // GET api/videos
        [HttpGet]
        public IEnumerable<VideoBO> Get()
        {
            return _bllFacade.VideoService.GetAll();
        }

        // GET api/videos/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var video = _bllFacade.VideoService.GetById(id);

            return video == null ? 
                NotFound($"Id: {id} - does not exist") : 
                new ObjectResult(video);
        }

        // POST api/videos
        [HttpPost]
        public IActionResult Post([FromBody] VideoBO video)
        {
            if (video == null) return BadRequest("JSON object not valid");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Created("", _bllFacade.VideoService.Create(video));
        }

        // PUT api/videos/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] VideoBO video)
        {
            if (id != video.Id)
                return BadRequest($"Id: {id} from url - does not match the entity id");

            var result = _bllFacade.VideoService.Update(video);

            if(result == null) return NotFound($"Id: {id} - doesn't exist");

            return Ok("Updated!");
        }

        // DELETE api/videos/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _bllFacade.VideoService.Delete(id);
            if (!deleted) return NotFound($"Id: {id} - doesn't exist");

            return Ok("Deleted");
        }
    }
}