using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;

namespace VideoAppDAL.Repository
{
    public class ProfileRepository : ARepository<Profile>
    {
        public ProfileRepository(DbContext context) : base(context)
        {
        }

        public override List<Profile> GetAllById(List<int> ids)
        {
            return ids == null ?
                null :
                Context.Set<Profile>().Where(g => ids.Contains(g.Id)).ToList();
        }
    }
}