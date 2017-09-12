using VideoAppBLL.BusinessObjects;

namespace VideoAppBLL.Interfaces
{
    public interface IBLLFacade
    {
        IVideoService VideoService { get; }

        IProfileService ProfileService { get; }

    }
}