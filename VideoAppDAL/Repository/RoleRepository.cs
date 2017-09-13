using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Entities;

namespace VideoAppDAL.Repository
{
    public class RoleRepository : Repository<Role>
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }
    }
}