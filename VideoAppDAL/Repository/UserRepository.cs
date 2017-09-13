using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;

namespace VideoAppDAL.Repository
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}