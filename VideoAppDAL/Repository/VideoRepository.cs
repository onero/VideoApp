using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Entities;

namespace VideoAppDAL.Repository
{
    internal class VideoRepository : ARepository<Video>
    {
        public VideoRepository(DbContext context) : base(context)
        {
        }

        public override IEnumerable<Video> GetAll()
        {
            return Context.Set<Video>().Include(v => v.Genres).ToList();
        }

        public override Video GetById(int id)
        {
            return Context.Set<Video>()
                .Include(v => v.Genres)
                .FirstOrDefault(v => v.Id == id);
        }

        public override List<Video> GetAllByIds(List<int> ids)
        {
            return ids == null ? null : Context.Set<Video>().Where(g => ids.Contains(g.Id)).ToList();
        }

        public override Video Update(Video entity)
        {
            var entityFromDb = GetById(entity.Id);
            if (entityFromDb == null) return null;
            return Context.Update(entity).Entity;
        }
    }
}