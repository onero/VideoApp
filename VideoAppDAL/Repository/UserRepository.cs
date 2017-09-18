using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;

namespace VideoAppDAL.Repository
{
    internal class UserRepository : ARepository<User>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public override List<User> GetAllByIds(List<int> ids)
        {
            return ids == null ?
                null :
                Context.Set<User>().Where(g => ids.Contains(g.Id)).ToList();
        }

        public override User Update(User entity)
        {
            var entityFromDb = GetById(entity.Id);
            if (entityFromDb == null) return null;
            return Context.Update(entity).Entity;
        }
    }
}