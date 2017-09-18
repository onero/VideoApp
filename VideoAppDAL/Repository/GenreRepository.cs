using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Entities;

namespace VideoAppDAL.Repository
{
    public class GenreRepository : ARepository<Genre>
    {
        public GenreRepository(DbContext context) : base(context)
        {

        }

        public override List<Genre> GetAllById(List<int> ids)
        {
            return ids == null ? 
                null : 
                Context.Set<Genre>().Where(g => ids.Contains(g.Id)).ToList();
        }
    }
}