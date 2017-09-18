using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Entities;

namespace VideoAppDAL.Repository
{
    internal class ProfileRepository : ARepository<Profile>
    {
        public ProfileRepository(DbContext context) : base(context)
        {
        }

        public override List<Profile> GetAllByIds(List<int> ids)
        {
            return ids == null ? null : Context.Set<Profile>().Where(g => ids.Contains(g.Id)).ToList();
        }

        public override Profile Update(Profile entity)
        {
            var entityFromDb = GetById(entity.Id);
            if (entityFromDb == null) return null;
            return Context.Update(entity).Entity;
        }
    }
}