using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Entities;

namespace VideoAppDAL.Repository
{
    public class GenreRepository : Repository<Genre>
    {
        public GenreRepository(DbContext context) : base(context)
        {

        }
    }
}