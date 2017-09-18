using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Entities;

namespace VideoAppDAL.Repository
{
    internal class GenreRepository : ARepository<Genre>
    {
        public GenreRepository(DbContext context) : base(context)
        {

        }

        public override List<Genre> GetAllByIds(List<int> ids)
        {
            return ids == null ? 
                null : 
                Context.Set<Genre>().Where(g => ids.Contains(g.Id)).ToList();
        }

        public override Genre Update(Genre entity)
        {
            var entityFromDb = GetById(entity.Id);
            if (entityFromDb == null) return null;
            return Context.Update(entity).Entity;
        }
    }
}