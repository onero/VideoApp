using System.Collections.Generic;
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
        public ProfilesController()
        {
            Service = new BLLFacade().ProfileService;
        }
    }
}