using VidepAppEntity;

namespace VideoAppBLL.Interfaces
{
    public interface IBLLFacade
    {
        IService<Video> VideoService { get; }
    }
}