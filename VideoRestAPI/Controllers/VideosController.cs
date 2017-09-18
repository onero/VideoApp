using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;

namespace VideoRestAPI.Controllers
{
    [Route("api/[controller]")]
    public class VideosController : AController<VideoBO>
    {
        public VideosController() : base(new BLLFacade().VideoService)
        {
        }
    }
}