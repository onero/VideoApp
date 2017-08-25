using VideoAppBLL.Service;
using VideoAppDAL;
using VidepAppEntity;

namespace VideoAppBLL
{
    public class BLLFacade
    {

        public IService<Video> VideoService => new VideoService(new DALFacadeMem());
    }
}