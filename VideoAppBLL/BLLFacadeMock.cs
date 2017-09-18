﻿using VideoAppBLL.Interfaces;
using VideoAppBLL.Service;
using VideoAppDAL;
using VideoAppDAL.Interfaces;

namespace VideoAppBLL
{
    public class BLLFacadeMock : IBLLFacade
    {
        private readonly IDALFacade _dalFacade;
        public IVideoService VideoService => new VideoService(_dalFacade);

        public IProfileService ProfileService => new ProfileService(_dalFacade);

        public IRentalService RentalService => new RentalService(_dalFacade);

        public IGenreService GenreService => new GenreService(_dalFacade);

        public IUserService UserService => new UserService(_dalFacade);

        public IRoleService RoleService => new RoleService(_dalFacade);

        public BLLFacadeMock()
        {
            _dalFacade = new DALFacadeMock(); 
        }
    }
}