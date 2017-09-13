using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using VideoAppBLL.Service;
using VideoAppDAL;
using VideoAppDAL.Interfaces;

namespace VideoAppBLL
{
    public class BLLFacade : IBLLFacade
    {
        private readonly IDALFacade _dalFacade;

        public BLLFacade()
        {
            _dalFacade = new DALFacade();
        }

        public IVideoService VideoService => new VideoService(_dalFacade);
        public IProfileService ProfileService => new ProfileService(_dalFacade);
        public IRentalService RentalService => new RentalService(_dalFacade);
        public IGenreService GenreService => new GenreService(_dalFacade);
    }
}