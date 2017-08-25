using Microsoft.EntityFrameworkCore;
using VidepAppEntity;

namespace VideoAppDAL.Interfaces
{
    public interface IContext
    {
        DbSet<Video> Videos { get; set; }
    }
}