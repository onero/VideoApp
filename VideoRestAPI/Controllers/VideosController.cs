using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public VideoBO Get(int id)
        {
            return _bllFacade.VideoService.GetById(id);
        }

        // POST api/videos
        [HttpPost]
        public void Post([FromBody]VideoBO video)
        {
            _bllFacade.VideoService.Create(video);
        }

        // PUT api/videos/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]VideoBO video)
        {

        }

        // DELETE api/videos/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _bllFacade.VideoService.Delete(id);
        }
    }
}
