using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Entities;
using System.Linq;

namespace VideoAppDAL.Repository
{
    internal class RoleRepository : ARepository<Role>
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }

        public override List<Role> GetAllByIds(List<int> ids)
        {
            return ids == null ?
                null :
                Context.Set<Role>().Where(g => ids.Contains(g.Id)).ToList();
        }

        public override Role Update(Role entity)
        {
            var entityFromDb = GetById(entity.Id);
            if (entityFromDb == null) return null;
            return Context.Update(entity).Entity;
        }
    }
}