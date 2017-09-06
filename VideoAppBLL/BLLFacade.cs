using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using VideoAppBLL.Service;
using VideoAppDAL;

namespace VideoAppBLL
{
    public class BLLFacade : IBLLFacade
    {
        public IService<VideoBO> VideoService => new VideoService(new DALFacadeMem());
        public IService<ProfileBO> ProfileService => new ProfileService(new DALFacadeMem());
    }
}