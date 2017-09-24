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
        public VideosController(IVideoService service = null)
        {
            Service = service ?? new BLLFacade().VideoService;
        }
    }
}