using VideoAppBLL.Service;

namespace VideoAppBLL.Interfaces
{
    public interface IBLLFacade
    {
        IVideoService VideoService { get; }
        IProfileService ProfileService { get; }
        IRentalService RentalService { get; }
        IGenreService GenreService { get; }
        IUserService UserService { get; }
        IRoleService RoleService { get; }
    }
}