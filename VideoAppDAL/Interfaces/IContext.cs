using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Entities;

namespace VideoAppDAL.Interfaces
{
    public interface IContext
    {
        DbSet<Video> Videos { get; set; }

        DbSet<Profile> Profiles { get; set; }
    }
}