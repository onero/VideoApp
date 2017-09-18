using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;

namespace VideoAppDAL.Repository
{
    public class UserRepository : ARepository<User>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public override List<User> GetAllById(List<int> ids)
        {
            return ids == null ?
                null :
                Context.Set<User>().Where(g => ids.Contains(g.Id)).ToList();
        }
    }
}