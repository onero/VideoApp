using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Entities;
using System.Linq;

namespace VideoAppDAL.Repository
{
    public class RoleRepository : ARepository<Role>
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }

        public override List<Role> GetAllById(List<int> ids)
        {
            return ids == null ?
                null :
                Context.Set<Role>().Where(g => ids.Contains(g.Id)).ToList();
        }
    }
}