using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Entities;

namespace VideoAppDAL.Interfaces
{
    public interface IVideoContext
    {
        DbSet<Video> Videos { set; get; }
        DbSet<Profile> Profiles { get; set; }
        DbSet<Rental> Rentals { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
    }
}