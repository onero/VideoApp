using VideoAppBLL.BusinessObjects;

namespace VideoAppBLL.Interfaces
{
    public interface IBLLFacade
    {
        IService<VideoBO> VideoService { get; }

        IService<ProfileBO> ProfileService { get; }

    }
}