using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Service;
using VideoAppDAL;

namespace VideoAppBLL
{
    public class BLLFacade
    {
        public IService<VideoBO> VideoService => new VideoService(new DALFacadeMem());
    }
}