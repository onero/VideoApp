using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoAppBLL;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;

namespace VideoRestAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Roles")]
    public class RolesController : AController<RoleBO>
    {
        public RolesController()
        {
            Service = new BLLFacade().RoleService;
        }
    }
}
