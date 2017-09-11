using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using VideoAppBLL.Service;
using VideoAppDAL;

namespace VideoAppBLL
{
    public class BLLFacade : IBLLFacade
    {
        public IVideoService VideoService => new VideoService(new DALFacadeMem());
        public IProfileService ProfileService => new ProfileService(new DALFacadeMem());
        public IRentalService RentalService => new RentalService(new DALFacadeMem());
    }
}